using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubNodeListPlusWiondow : PopupWindowTemplate{
    public GameObject noItem;

    private void Start(){
        base.startPotion   = new Vector2(0,30);
        base.endPotion     = new Vector2(0,0);
        base.animationTime = 0.4f;
    }

    protected override void InEffectStartEvent(){

    }

}