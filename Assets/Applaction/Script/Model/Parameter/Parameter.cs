using System.Collections;
using System.Collections.Generic;

public enum ParameterType{
    none, subNode, number, sentence, dropdown, color,nodes
}


public class Parameter{
    public ParameterType type;
    public string Tag { get; protected set; }
    protected string value;
    public string Value { get { return GetValue(); } set { this.SetValue(value); } }
    public string Text { get; protected set; }
    public string Title { get; protected set; }

    public Parameter(ParameterType type, Dictionary<string, string> dic){
        this.type = type;
        if (dic.ContainsKey("tag"))  this.Tag = dic["tag"];
        if (dic.ContainsKey("text")) this.Text = dic["text"];
        if (dic.ContainsKey("value")) this.Value = dic["value"];
        if (dic.ContainsKey("title")) this.Title = dic["title"];
    }

    public Parameter Clone(){
        return (Parameter)MemberwiseClone();
    }


    protected virtual string GetValue(){
        return this.value;
    }

    protected virtual string SetValue(string str){
        this.value = str;
        return this.value;
    }
}

