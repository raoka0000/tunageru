using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using UnityEngine;

public class DebugTest : MonoBehaviour {
    BaseNode baseNode;
    public SidelistViewModel sidelist;
    public TextAsset input;


    public void Start(){
        this.baseNode = Import.LoadBaseNode();
        sidelist.AddBaseNode(this.baseNode);
    }

    public void test(){
        //ConfirmViewModel.instance.ShowWindow(() => Debug.Log("OK"), () => Debug.Log("NO"), "タイトルです", "てすとてすとてすと");
        //TunagumIO.LoadFromPath();

        //BaseBox bb = TunagumIO.LoadBaseBox();
        //string optionText = Import.ReadFile(DEFINE.OptionFileName);
        //var bb = BaseBox.ConvertOptionText(optionText);
        var bb = BaseBox.ConvertOptionText(input.text);

        BoxViewModel.instance.SetBaseBox(bb);
    }



}
