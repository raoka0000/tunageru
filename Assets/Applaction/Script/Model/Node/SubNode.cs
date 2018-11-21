﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;


public class SubNode : Node{

    public string tag;

    public SubNode(string nodeText) : base(nodeText){
        this.tag = nodeText.Between("[<tag>]", "[/<tag>]").Trim();
    }

    public SubNode Clone(){
        return (SubNode)MemberwiseClone();
    }

}
