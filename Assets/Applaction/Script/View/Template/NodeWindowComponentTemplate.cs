using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeWindowComponentTemplate<C,P> : MonoBehaviour where C : NodeWindowComponent where P : Parameter{

   /* public Text title;
    public Text dscription;
    public GameObject dscriptionObj;

    public static C Instantiate(GameObject prefab, GameObject parent, P parameter){
        C obj = Instantiate(prefab, parent.transform).GetComponent<C>();
        obj.parameter = parameter;
        obj.title.text = parameter.Title;
        if (parameter.Text == null)
        {
            obj.dscriptionObj.SetActive(false);
        }
        else
        {
            obj.dscription.text = parameter.Text;
        }
        if (parameter.Value != null)
        {
            obj.inputField.text = parameter.Value;
        }
        return obj;
    }*/

}
