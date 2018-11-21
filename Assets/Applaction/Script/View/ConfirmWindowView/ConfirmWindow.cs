using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ConfirmWindow : PopupWindowTemplate{
    [SerializeField]
    private Text textObj, titleObj;
    private string _text = "";
    public string text{
        get{
            return this._text;
        }
        set{
            this._text = value;
            textObj.text = this._text;
        }
    }
    private string _title = "";
    public string title{
        get{
            return this._title;
        }
        set{
            this._title = value;
            titleObj.text = this._title;
        }
    }

    public UnityAction okEvent;
    public UnityAction cancelEvent;

    protected override void OutEffectEndEvent(){
        okEvent = null;
        cancelEvent = null;
        text = "";
        title = "";
    }

    public void PushedOK(){
        if(okEvent != null) okEvent();
        OutEffect();
    }

    public void PushedCancel(){
        if(cancelEvent != null) cancelEvent();
        OutEffect();
    }
}
