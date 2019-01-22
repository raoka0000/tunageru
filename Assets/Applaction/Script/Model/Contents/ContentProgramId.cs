using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentProgramId : Content {

    public ContentProgramId(Box parent, ContentType type, Dictionary<string, string> attributes) : base(parent, type, attributes){

    }

    protected override string GetValue(){
        return this.parentBox.programId;
    }
}
