using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentChildId : Content {

    public ContentChildId(Box parent, ContentType type, Dictionary<string, string> attributes) : base(parent, type, attributes){

    }

    protected override string GetValue(){
        string s = "";
        foreach (Box b in this.parentBox.childBox){
            s += b.id.ToString() + ",";
        }
        return s.TrimEnd(',');

    }
}
