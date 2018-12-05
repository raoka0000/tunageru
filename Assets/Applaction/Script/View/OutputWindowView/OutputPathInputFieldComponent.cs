using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class OutputPathInputFieldComponent : MonoBehaviour{
    public InputField inputField;

    void Start(){
        var pathEvent = gameObject.ObserveEveryValueChanged(_ => OutputViewModel.instance.OutputPath);
        pathEvent.Subscribe(x => OnChangedOutputPathText(x));
        //inputField.text = OutputViewModel.instance.OutputPath;
    }

    public void Edited(string str){
        OutputViewModel.instance.OutputPath = str;
    }

    public void OnChangedOutputPathText(string str){
        inputField.text = OutputViewModel.instance.OutputPath;
    }

}
