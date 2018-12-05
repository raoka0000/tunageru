using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NodeWindow : MonoBehaviour {
    //拡大縮小のアニメーション
    private const float animationTime = 0.5f;
    private Color baseNodeColor = new Color(0.94f, 1f, 1f);
    private Vector2 smallSize = new Vector2(100, 100);
    private Vector2 bigSize = new Vector2(500, 150);

    public Node node { get; set; }
    private NodeWindow _parentWindow;
    public NodeWindow parentWindow { get { return _parentWindow; } set { SetParentWindow(value); } }
    [System.NonSerialized]
    public List<NodeWindow> childWindows = new List<NodeWindow>();
    [System.NonSerialized]
    public List<NodeWindowComponent> componentList = new List<NodeWindowComponent>();

    public GameObject removeButton;

    public Text titleText;
    public RectTransform titleRect;
    public Text description;
    public Image icon;
    public ContentSizeFitter sizeFitter;
    public RectTransform maskReact;
    public Image bg;
    public Image bgTitle;

    public GameObject contentsObject;

    [System.NonSerialized]
    public bool isOpen = true;

    public static NodeWindow Instantiate(GameObject prefab, GameObject parent, Node node){
        NodeWindow obj = Instantiate(prefab, parent.transform).GetComponent<NodeWindow>();
        obj.node = node;
        if (node.color != ""){
            obj.SetColor(node.color);
        }else if (node is BaseNode){
            obj.bg.color = obj.baseNodeColor;
            obj.bgTitle.color = obj.baseNodeColor;
        }
        obj.removeButton.SetActive(node.optional);
        obj.titleText.text = node.title;
        obj.description.text = node.text;
        obj.icon.sprite = ImageIO.GetIcon(node.iconName);
        obj.SetComponentForParameter();
        return obj;
    }

    private void SetComponentForParameter(){
        List<SubNodeParameter> subNodeParameters = new List<SubNodeParameter>();
        foreach(Parameter p in this.node.parameters){
            //Number
            if(p.type == ParameterType.number){
                NumberParameter pram = p as NumberParameter;
                var compo = NumberComponent.Instantiate(MainViweModel.instance.NumberComponentPrefab, contentsObject, pram);
                componentList.Add(compo);
            }

            //sentence
            if (p.type == ParameterType.sentence){
                SentenceParameter pram = p as SentenceParameter;
                var compo = SentenceComponent.Instantiate(MainViweModel.instance.SentenceComponentPrefab, contentsObject, pram);
                componentList.Add(compo);
            }

            //dropdown
            if (p.type == ParameterType.dropdown){
                DropdownParameter pram = p as DropdownParameter;
                var compo = DropdownComponent.Instantiate(MainViweModel.instance.DropdownComponentPrefab, contentsObject, pram);
                componentList.Add(compo);
            }

            //nodes
            if(p.type == ParameterType.nodes){
                NodesParameter pram = p as NodesParameter;
                var compo = SubNodeListComponent.Instantiate(MainViweModel.instance.SubNodeListComponentPrefab, contentsObject, pram.list, this);
                componentList.Add(compo);
                if(pram.Title != null)compo.title.text = pram.Title;
            }

            //subNodeList
            if (p.type == ParameterType.subNode){
                subNodeParameters.Add(p as SubNodeParameter);
            }
        }

        if (subNodeParameters.Count != 0){
            var compo = SubNodeListComponent.Instantiate(MainViweModel.instance.SubNodeListComponentPrefab, contentsObject, subNodeParameters, this);
            componentList.Add(compo);
        }

    }

    public void Start(){
        //this.bigSize = maskReact.sizeDelta;
        foreach(NodeWindowComponent compo in this.componentList){
            if(compo is SubNodeListComponent){
                SubNodeListComponent snlc = compo as SubNodeListComponent;
            }
        }
    }


    public void OnDoubleClick(){
        if(isOpen){
            Close();
        }else{
            Open();
        }
    }


    public void Close(){
        Sequence sequence = DOTween.Sequence();
        sizeFitter.enabled = false;
        this.bigSize = maskReact.sizeDelta;
        var titlePos = new Vector2(smallSize.x / 2 + 20, 0);
        sequence.Join(titleRect.DOAnchorPos(titlePos, animationTime * 1.2f).SetEase(Ease.InElastic))
                .Join(maskReact.DOSizeDelta(smallSize, animationTime).SetEase(Ease.InOutBack))
                .OnComplete(() => isOpen = false);
        sequence.Play();
    }

    public void Open(){
        Sequence sequence = DOTween.Sequence();
        var titlePos = new Vector2(titleRect.sizeDelta.x - 10, 0);
        sequence.Join(titleRect.DOAnchorPos(titlePos, animationTime * 1.2f).SetEase(Ease.InElastic))
                .Join(maskReact.DOSizeDelta(bigSize, animationTime).SetEase(Ease.InOutBack))
                .OnComplete(() => { isOpen = true; sizeFitter.enabled = true; });
        sequence.Play();
    }

    public void Remove(){
        MainViweModel.instance.RemoveWindow(node);
    }

    private void SetParentWindow(NodeWindow window){
        this._parentWindow = window;
        this._parentWindow.childWindows.Add(this);
    }

    public void SetColor(string color){
        Color c = color.ToColor();
        this.SetColor(c);
    }

    public void SetColor(Color color){
        this.bg.color = color;
        this.bgTitle.color = color;
    }

}
