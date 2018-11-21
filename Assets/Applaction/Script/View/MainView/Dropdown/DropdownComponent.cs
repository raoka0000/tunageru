using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownComponent : NodeWindowComponent{
    public DropdownParameter parameter;

    public Text title;
    public Text dscription;
    public GameObject dscriptionObj;
    public Dropdown dropdown;


    public static DropdownComponent Instantiate(GameObject prefab, GameObject parent, DropdownParameter parameter){
        DropdownComponent obj = Instantiate(prefab, parent.transform).GetComponent<DropdownComponent>();
        obj.parameter = parameter;
        if (parameter.Title == null){
            obj.title.gameObject.SetActive(false);
        }else{
            obj.title.text = parameter.Title;
        }

        if (parameter.Text == null){
            obj.dscriptionObj.SetActive(false);
        }else{
            obj.dscription.text = parameter.Text;
        }
        obj.dropdown.ClearOptions();
        obj.dropdown.AddOptions(parameter.list);
        return obj;
    }

    public void Edited(int i){
        string str = dropdown.options[i].text;
        this.parameter.Value = str;
    }

}
