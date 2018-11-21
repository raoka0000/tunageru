using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubNodeListPlusUnit : MonoBehaviour {
    public Image icon;
    public Text title;

    private SubNodeParameter parameter;
    private Node node { get { return parameter.subNode; } }
    private SubNodeListComponent subNodeComponent;


    public static SubNodeListPlusUnit Instantiate(GameObject prefab, GameObject parent, SubNodeParameter parameter, SubNodeListComponent subNodeComponent){
        SubNodeListPlusUnit obj = Instantiate(prefab, parent.transform).GetComponent<SubNodeListPlusUnit>();
        obj.subNodeComponent = subNodeComponent;
        obj.parameter = parameter;
        obj.icon.sprite = ImageIO.GetIcon(obj.node.iconName);
        obj.title.text = obj.node.title;
        return obj;
    }

    public void Pushed(){
        subNodeComponent.AddUnit(this.parameter);
        Destroy(this.gameObject);
    }
}
