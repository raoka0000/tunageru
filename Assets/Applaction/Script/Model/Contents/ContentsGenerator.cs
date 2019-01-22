using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentsGenerator{
    
    public static List<Content> ContentsList(Box parent, string text){
        var list = new List<Content>();
        string[] lines = text.SplitLine();
        foreach (string str in lines){
            if (str.IsPresent()){
                var content = CreateContent(parent, str);
                list.Add(content);
            }
        }
        return list;
    }

    public static Dictionary<string, string> CreateAttributes(string text){
        string rawText = text.TrimStart('[').TrimEnd(']');
        //attributesの作成
        Dictionary<string, string> attr = new Dictionary<string, string>();
        //Debug.Log(rawText);
        foreach (string str in rawText.SplitSpace()){
            string[] s = str.Split('=');
            if (s.Length >= 2){
                attr[s[0]] = s[1];
            }
        }

        return attr;
    }


    public static Content CreateContent(Box parent, string data){
        Dictionary<string, string> attr = CreateAttributes(data);
        ContentType type = ContentType.none;
        if (attr.ContainsKey("type")){
            type = ContentTypeUtility.ConvertStringToContentType(attr["type"]);
        }

        return type.GetContent(parent,attr);
    }

}
