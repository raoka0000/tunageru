using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class BoxRadioButtonConponent : BoxViewComponent<BoxRadioButtonConponent> {
    public ToggleGroup toggleGroup;
    public Toggle toggleTamplate;
    public Dictionary<Toggle, string> toggleDic = new Dictionary<Toggle, string>();

    public override void init(Content content){
        base.init(content);
        if (content.list != null){
            foreach(string item in content.list){
                var toggle = Instantiate(toggleTamplate, toggleGroup.gameObject.transform);
                toggle.gameObject.SetActive(true);
                var texts = toggle.GetComponentsInChildren<Text>();
                foreach(Text t in texts){
                    t.text = item;
                }
                toggleDic.Add(toggle, item);
            }
        }
        if(content.value.IsPresent()){
            Toggle toggle = toggleDic.First(x => x.Value == content.value).Key;
            toggle.isOn = true;
        }else{
            toggleDic.First().Key.isOn = true;
        }
    }

    public void Edited(bool b){
        foreach(Toggle t in toggleGroup.ActiveToggles()){
            if(toggleDic.ContainsKey(t)){
                this.content.value = toggleDic[t];
            }
        }
    }
}
