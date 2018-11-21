using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SubNodeListComponentUnit : MonoBehaviour{
    private SubNodeParameter parameter;
    public Node node { get { return parameter.subNode; } }
    public Image icon;
    public Text title;
    public GameObject removeButtonobj;

    private RectTransform _rectTransform;
    private RectTransform rectTransform { 
        get{
            if(this._rectTransform == null){
                this._rectTransform = this.gameObject.GetComponent<RectTransform>();
            }
            return this._rectTransform;
        }
    }


    public static SubNodeListComponentUnit Instantiate(GameObject prefab, GameObject parent, SubNodeParameter parameter){
        SubNodeListComponentUnit obj = Instantiate(prefab, parent.transform).GetComponent<SubNodeListComponentUnit>();
        obj.parameter = parameter;
        if (parameter.optional) obj.removeButtonobj.SetActive(true);
        obj.title.text = obj.node.title ?? "no title";
        obj.icon.sprite = ImageIO.GetIcon(obj.node.iconName);
        return obj;
    }

    public void Pushed(){
        //SubNodeListComponent comp = this.transform.parent.GetComponent<SubNodeListComponent>();
        //MainViweModel.instance.AddWindow(node, false, comp.prentWindow);
        MainViweModel.instance.MoveToNodeWindow(node);
    }

    public void PushedRemoveBouttan(){
        this.rectTransform.DOScale(0.01f, 0.8f).SetEase(Ease.InOutElastic)
            .OnComplete(()=>Destroy(this.gameObject));
    }

}
