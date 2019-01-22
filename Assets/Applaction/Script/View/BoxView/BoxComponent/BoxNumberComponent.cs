using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxNumberComponent : BoxViewComponent<BoxNumberComponent> {
    public InputField inputField;

	public override void init(Content content){
        base.init(content);
        if (content.value != null){
            this.inputField.text = content.value;
        }

	}

	public void Edited(string str){
        this.content.value = str;
    }

}
