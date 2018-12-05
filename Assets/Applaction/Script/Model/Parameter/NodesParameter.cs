using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodesParameter : Parameter{
    public List<SubNodeParameter> list = new List<SubNodeParameter>();

    public NodesParameter(ParameterType type, Dictionary<string, string> dic) : base(type, dic){
        if (dic.ContainsKey("list")){
            foreach (string str in dic["list"].Split(',')){
                Dictionary<string, string> d = new Dictionary<string, string>(){
                    {"tag",str},
                    {"optional","true"}
                };
                SubNodeParameter p = new SubNodeParameter(ParameterType.subNode,d);
                this.list.Add(p);
            }
        }
    }

    protected override string GetValue(){
        string str = "";
        foreach(SubNodeParameter p in list){
            if(p.canOutput){
                str += "\n" + p.Value;
            }
        }
        return str;
    }

}
