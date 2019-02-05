using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UniRx;

public class BoxWindow : MonoBehaviour {
    //拡大縮小のアニメーション
    private const float animationTime = 0.5f;
    private Color baseBoxColor = new Color(0.94f, 1f, 1f);
    private Vector2 smallSize = new Vector2(100, 100);
    private Vector2 bigSize = new Vector2(500, 150);

    public Box box { get; set; }
    //public ReactiveProperty<Box> box { get; set; }

    /*
    private BoxWindow _parentWindow;
    public BoxWindow parentWindow { get { return _parentWindow; } set { SetParentWindow(value); } }
    [System.NonSerialized]
    public List<BoxWindow> childWindows = new List<BoxWindow>();*/

    public RectTransform rectTransform;

    public GameObject removeButton;

    public Text titleText;
    public RectTransform titleRect;
    public Text description;
    public Image icon;
    public ContentSizeFitter sizeFitter;
    public RectTransform maskReact;
    public Image bg;
    public Image bgTitle;

    public GameObject Connector;

    public RectTransform lineAnchorParent;
    public RectTransform lineAnchorChild;

    public CanvasGroup canvas;

    public GameObject contentsObject;

    [System.NonSerialized]
    public bool isOpen = true;
    [System.NonSerialized]
    public bool connectModeFlag = false;

    private Color defaultColor = new Color(1, 1, 1);

    public DragShakeAttribute dragAttribute;


    public BoxCollider2D boxCollider;


	public static BoxWindow Instantiate(GameObject prefab, GameObject parent, Box box){
        BoxWindow obj = Instantiate(prefab, parent.transform).GetComponent<BoxWindow>();
        obj.box = box;
        if (box.color != ""){
            Color c = box.color.ToColor();
            obj.defaultColor = c;
        }
        else if (box is BaseBox){
            obj.defaultColor = obj.baseBoxColor;
        }
        obj.SetColor(obj.defaultColor);

        obj.removeButton.SetActive(box.optional);
        obj.titleText.text = box.title;
        obj.description.text = box.text;
        obj.icon.sprite = ImageIO.GetIcon(box.iconName);
        obj.SetContents();

        foreach(Box b in box.parentBox){
            obj.ConnectLine(b);
        }

        obj.box.parentBox
           .ObserveAdd()
           .Subscribe(x => obj.ConnectLine(x.Value));

        obj.box.parentBox
           .ObserveRemove()
           .Subscribe(x => obj.DisconnectLine(x.Value));
        
        obj.HideConnector();

        return obj;
    }

    private void SetContents(){
        foreach(Content c in this.box.contents){
            if(c.type == ContentType.number){
                BoxNumberComponent.Instantiate(BoxViewModel.instance.numberComponent.gameObject, contentsObject, c);
            }
            if (c.type == ContentType.text){
                BoxTextComponent.Instantiate(BoxViewModel.instance.textComponent.gameObject, contentsObject, c);
            }
            if (c.type == ContentType.radio){
                BoxRadioButtonConponent.Instantiate(BoxViewModel.instance.raidoComponent.gameObject, contentsObject, c);
            }
            if (c.type == ContentType.toggle){
                BoxToggleComponent.Instantiate(BoxViewModel.instance.toggleComponent.gameObject, contentsObject, c);
            }

        }
    }

    public void ConnectLine(Box box){
        var win = BoxViewModel.instance.SearchBoxWindow(box);
        if (win != null){
            //LineViewModel.instance.AddLine(this.lineAnchorParent, win.lineAnchorChild);
            LineViewModel.instance.AddLine(this.gameObject, win.gameObject);
        }

    }

    public void DisconnectLine(Box box){
        var win = BoxViewModel.instance.SearchBoxWindow(box);
        if (win != null){
            LineViewModel.instance.RemoveLine(this.gameObject, win.gameObject);
        }
    }


    public void HideConnector(){
        this.Connector.SetActive(false);
    }

    public void ShowConnector(){
        this.Connector.SetActive(true);
    }


    public void Start(){
        this.rectTransform
            .ObserveEveryValueChanged(arg1 => arg1.rect)
            .Subscribe(_ => ChangedMaskReact());
    }


    public void OnDoubleClick(){
        if (isOpen){
            Close();
        }
        else{
            Open();
        }
    }

    private void ChangedMaskReact(){
        this.boxCollider.size = new Vector2(rectTransform.rect.width, rectTransform.rect.height);
    }


    public void Close(){
        Sequence sequence = DOTween.Sequence();
        sizeFitter.enabled = false;
        this.bigSize = maskReact.sizeDelta;
        var titlePos = new Vector2(smallSize.x / 2 + 20, -24);
        sequence.Join(titleRect.DOAnchorPos(titlePos, animationTime * 1.2f).SetEase(Ease.InElastic))
                .Join(maskReact.DOSizeDelta(smallSize, animationTime).SetEase(Ease.InOutBack))
                .OnComplete(() => isOpen = false);
        sequence.Play();
    }

    public void Open(){
        Sequence sequence = DOTween.Sequence();
        var titlePos = new Vector2(titleRect.sizeDelta.x - 10, -24);
        sequence.Join(titleRect.DOAnchorPos(titlePos, animationTime * 1.2f).SetEase(Ease.InElastic))
                .Join(maskReact.DOSizeDelta(bigSize, animationTime).SetEase(Ease.InOutBack))
                .OnComplete(() => { isOpen = true; sizeFitter.enabled = true; });
        sequence.Play();
    }

    public void Remove(){
        if(this.box == null){
            RemoveAnime();
        }else{
            BoxViewModel.instance.RemoveBox(this.box);
            RemoveAnime();
        }
    }

    public void RemoveAnime(){
        Sequence sequence = DOTween.Sequence();
        sizeFitter.enabled = false;
        this.bigSize = maskReact.sizeDelta;
        var titlePos = new Vector2(smallSize.x / 2 + 20, -24);
        sequence.Join(titleRect.DOSizeDelta(new Vector2(0,0), animationTime * 1.2f).SetEase(Ease.InElastic))
                .Join(maskReact.DOSizeDelta(new Vector2(0,0), animationTime).SetEase(Ease.InOutBack))
                .OnComplete(() => Destroy(this.gameObject));
        sequence.Play();
    }

    /*private void SetParentWindow(BoxWindow window){
        this._parentWindow = window;
        this._parentWindow.childWindows.Add(this);
    }*/

    public void SetColor(Color color){
        this.bg.color = color;
        this.bgTitle.color = color;
    }

    public void ResetColor(){
        SetColor(defaultColor);
    }

    public void GoConnectModeForParent(){
        BoxViewModel.instance.GoConnectMode(this.box, BoxViewModel.mode.connectPrent);
    }

    public void GoConnectModeForChild(){
        BoxViewModel.instance.GoConnectMode(this.box, BoxViewModel.mode.connectchild);
    }

    public void GoNomalMode(){
        BoxViewModel.instance.GoNomalMode();
    }

    public void OnDrop(){
        BoxViewModel.instance.ConnectDrop(this.box);
    }


    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "box"){
            BoxWindow bw = other.GetComponent<BoxWindow>();
            if(bw != null && bw.dragAttribute.isDraging && this.dragAttribute.isDraging){
                BoxViewModel.instance.Connect(bw.box, this.box);
            }
        }
    }

}
