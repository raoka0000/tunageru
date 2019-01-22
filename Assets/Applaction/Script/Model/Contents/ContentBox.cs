using System.Collections;
using System.Collections.Generic;

public class ContentBox : Content {

    public ContentBox(Box parent, ContentType type, Dictionary<string, string> attributes) : base(parent, type, attributes){
        
    }

	protected override string GetValue(){
        string str = "";
        foreach(Box box in parentBox.childBox){
            if(this.tag == box.tag){
                str = box.value;
            }
        }
        return str;
	}

}
