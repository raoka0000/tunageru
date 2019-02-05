using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentText : Content {
    public bool newline = false;

    public ContentText(Box parent, ContentType type, Dictionary<string, string> attributes) : base(parent, type, attributes){
        if(attributes.ContainsKey("newline") && attributes["newline"] == "yes"){
            newline = true;
        } 
    }

}
