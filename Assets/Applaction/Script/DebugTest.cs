using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using UnityEngine;

public class DebugTest : MonoBehaviour {
    BaseNode baseNode;
    public SidelistViewModel sidelist;

    public void Start(){
        this.baseNode = Import.LoadBaseNode();
        sidelist.AddBaseNode(this.baseNode);
    }

    public void test(){
        ConfirmViewModel.instance.ShowWindow(() => Debug.Log("OK"), () => Debug.Log("NO"), "タイトルです", "てすとてすとてすと");
    }
}
