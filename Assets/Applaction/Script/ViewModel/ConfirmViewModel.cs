using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ConfirmViewModel : SingletonMonoBehaviour<ConfirmViewModel>{
    [SerializeField]
    private ConfirmWindow window;
    public bool isOpen{
        get{
            return window.isOpen;
        }
    }

    public void ShowWindow(UnityAction okEvent, UnityAction cancelEvent, string title, string text){
        if (isOpen == false )
        {
            this.window.okEvent = okEvent;
            this.window.cancelEvent = cancelEvent;
            this.window.title = title;
            this.window.text = text;
            this.window.InEffect();
        }
    }

    public void HideWiondow(){
        if (isOpen == true)
        {
            this.window.OutEffect();
        }
    }


}
