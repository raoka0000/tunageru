using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;


public class SubNode : Node{


    public SubNode(string nodeText) : base(nodeText){
        
    }

    public SubNode Clone(){
        return new SubNode(this.nodeText);
    }

    protected override string getValue(){
        string outText = base.getValue();
        foreach (Parameter p in parameters){
            outText = outText.Replace(p.Tag, "");
        }
        return outText;
    }

}
