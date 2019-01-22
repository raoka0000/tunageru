using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxToggleComponent : BoxViewComponent<BoxToggleComponent> {
    public Toggle toggle;

    public override void init(Content content){
        base.init(content);
        if (content.value == "on"){
            this.toggle.isOn = true;
        }

    }

    public void Edited(){
        this.content.value = this.toggle.isOn.ToString();
    }

}
