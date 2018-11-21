using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubNodeListPlus : MonoBehaviour {
    public SubNodeListPlusWiondow window;
    private SubNodeListComponent subNodeComponent;

    public static SubNodeListPlus Instantiate(GameObject prefab, GameObject parent, SubNodeListComponent subNodeComponent){
        SubNodeListPlus obj = Instantiate(prefab, parent.transform).GetComponent<SubNodeListPlus>();
        obj.subNodeComponent = subNodeComponent;
        obj.transform.SetAsFirstSibling();
        return obj;
    }

    public SubNodeListPlusUnit AddUnit(SubNodeParameter parameter){
        return SubNodeListPlusUnit.Instantiate(MainViweModel.instance.SubNodeListPlusUnitPrefab, window.gameObject, parameter, subNodeComponent);
    }

    public void Pushed(){
        window.InEffect();
    }


}
