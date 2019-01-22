using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentId : Content {
    
    public ContentId(Box parent, ContentType type, Dictionary<string, string> attributes):base(parent, type, attributes){
        
    }

	protected override string GetValue(){
        return this.parentBox.id.ToString();
	}

}
