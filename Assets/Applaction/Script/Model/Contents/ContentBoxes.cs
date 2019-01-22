using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBoxes : Content {

    private string header;
    private string footer;

    public ContentBoxes(Box parent, ContentType type, Dictionary<string, string> attributes) : base(parent, type, attributes){
        if (this.attributes.ContainsKey("header")){
            this.header = this.attributes["header"];
        }
        if (this.attributes.ContainsKey("footer")){
            this.footer = this.attributes["footer"];
        }


    }

	protected override string GetValue(){
        string str = "";

        foreach(Box box in parentBox.childBox){
            //魔法の力で解決
            if (this.header == "TunagumH"){
                str += "<program id=\"" + box.programId + "\">";
            }

            if(list.Contains(box.tag)){
                if (str.IsPresent()) str += '\n'; 
                str += box.value;
            }

            if (this.footer == "TunagumF"){
                str += "\n</program>\n";
            }

        }

        return str;
	}


}
