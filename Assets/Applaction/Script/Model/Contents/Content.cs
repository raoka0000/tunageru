using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Content {
    private string originText;
    public Dictionary<string, string> attributes;
    public ContentType type;
    public Box parentBox;

    protected string _value = "";
    public string value { get { return _GetValue(); } set { SetValue(value); } }//出力される文字列
    public string tag { get; protected set; }//タグ 
    public string title { get; protected set; }//コンテンツの表示する名前
    public string desc { get; protected set; }//コンテンツの説明
    public string cond { get; protected set; }//if　条件 コンディションの略
    public string[] list { get; protected set; }//カンマ区切りの文字列

    public Content(Box parent, ContentType type, Dictionary<string,string> attributes){
        this.parentBox  = parent;
        this.type = type;
        this.attributes = attributes;

        if (this.attributes.ContainsKey("value")) this.value = this.attributes["value"];
        if (this.attributes.ContainsKey("tag"))   this.tag   = this.attributes["tag"];
        if (this.attributes.ContainsKey("title")) this.title = this.attributes["title"];
        if (this.attributes.ContainsKey("desc"))  this.desc  = this.attributes["desc"];
        if (this.attributes.ContainsKey("if"))    this.cond  = this.attributes["if"];
        if (this.attributes.ContainsKey("list"))  this.list  = this.attributes["list"].Split(',');

    } 


    protected virtual string _GetValue(){
        /*if(this.cond != null){
            if(this.cond == "top"){
                if(parentBox.id != 0){
                    return "";
                }
            }
        }*/
        return this.GetValue();
    }

    protected virtual string GetValue(){
        return this._value;
    }

    protected virtual string SetValue(string str){
        return this._value = str;
    }
}
