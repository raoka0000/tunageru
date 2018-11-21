using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutputPathInputFieldComponent : MonoBehaviour {
    public InputField inputField;

    void Start(){
        inputField.text = OutputViewModel.instance.OutputPath;
    }

    public void Edited(string str){
        OutputViewModel.instance.OutputPath = str;
    }

}
