using System;
using System.Collections;
using System.Collections.Generic;


public enum ContentType{
    none, box, boxes, number, text, dropdown,radio,toggle, color,id,nextid,programId
}


public static partial class ContentTypeExtend{
    public static string GetTypeName(this ContentType type){
        return type.ToString();
    }

    public static Content GetContent(this ContentType type, Box parent, Dictionary<string, string> attributes){
        switch (type){
            case ContentType.box:
                return new ContentBox(parent, type, attributes);
            case ContentType.boxes:
                return new ContentBoxes(parent, type, attributes);
            case ContentType.id:
                return new ContentId(parent, type, attributes);
            case ContentType.nextid:
                return new ContentChildId(parent, type, attributes);
            case ContentType.programId:
                return new ContentProgramId(parent, type, attributes);
            case ContentType.number:
                return new ContentNumber(parent, type, attributes);
            case ContentType.text:
                return new ContentText(parent, type, attributes);
            case ContentType.dropdown:
                return new ContentDropdown(parent, type, attributes);
            case ContentType.radio:
                return new ContentRadio(parent, type, attributes);
            case ContentType.toggle:
                return new ContentToggle(parent, type, attributes);//TODO:あとで;
        }

        return new Content(parent, type, attributes);
    }
}

public sealed class ContentTypeUtility{
    public static ContentType ConvertStringToContentType(string name){
        if (name.IsBlank()) return ContentType.none;
        ContentType ct = (ContentType)Enum.Parse(typeof(ContentType), name);
        return ct;
    }

}
