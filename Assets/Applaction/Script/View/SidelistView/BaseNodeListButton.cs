using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseNodeListButton : MonoBehaviour {
    public BaseNode _baseNode;
    public BaseNode baseNode { set { SetBaseNode(value); } get { return _baseNode; } }
    public Image icon;
    public Text label;

    public static BaseNodeListButton Instantiate(GameObject prefab, GameObject parent, BaseNode baseNode){
        BaseNodeListButton obj = Instantiate(prefab, parent.transform).GetComponent<BaseNodeListButton>();
        obj.baseNode = baseNode;
        return obj;
    }

    void SetBaseNode(BaseNode baseNode){
        this._baseNode = baseNode;
        this.icon.sprite = ImageIO.GetIcon(baseNode.iconName);
        this.icon.enabled = (this.icon.sprite != null);
        this.label.text = baseNode.title;
    }

    public void Pushed(){
        MainViweModel.instance.OpenNewBaseNode(this.baseNode);
    }
}
