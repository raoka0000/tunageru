﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberComponent : NodeWindowComponent{
    public NumberParameter parameter;

    public Text title;
    public Text dscription;
    public GameObject dscriptionObj;
    public InputField inputField;

    public static NumberComponent Instantiate(GameObject prefab, GameObject parent, NumberParameter parameter){
        NumberComponent obj = Instantiate(prefab, parent.transform).GetComponent<NumberComponent>();
        obj.parameter = parameter;
        if(parameter.Title == null){
            obj.title.gameObject.SetActive(false);
        }else{
            obj.title.text = parameter.Title;
        }
        if(parameter.Text == null){
            obj.dscriptionObj.SetActive(false);
        }else{
            obj.dscription.text = parameter.Text;
        }
        if(parameter.Value != null){
            obj.inputField.text = parameter.Value;
        }
        return obj;
    }

    public void Edited(string str){
        this.parameter.Value = str;
    }
}
