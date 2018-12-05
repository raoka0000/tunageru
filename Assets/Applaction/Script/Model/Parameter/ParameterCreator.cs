using System.Collections;
using System.Collections.Generic;

public class ParameterCreator{

    static public ParameterType ConvertToParameterType(string s){
        switch(s){
            case "sub":
                return ParameterType.subNode;
            case "number":
                return ParameterType.number;
            case "sentence":
                return ParameterType.sentence;
            case "dropdown":
                return ParameterType.dropdown;
            case "color":
                return ParameterType.color;
            case "nodes":
                return ParameterType.nodes;
            default:
                return ParameterType.none;
        }
    }

    static public List<Parameter> GetParameters(string pramText){
        var param = new List<Parameter>();
        string[] line = pramText.SplitLine();
        foreach(string str in line){
            if(str != "")param.Add(CreateParameter(str));
        }
        return param;
    }

    static public Parameter CreateParameter(string data){
        Dictionary<string, string> dic = new Dictionary<string, string>();
        foreach(string str in data.TrimStart('[').TrimEnd(']').SplitSpace()){
            string[] s = str.Split('=');
            if(s.Length >= 2){
                dic[s[0]] = s[1];
            }
        }
        if(dic.ContainsKey("type")){
            var type = ConvertToParameterType(dic["type"]);
            switch (type){
                case ParameterType.subNode:
                    return new SubNodeParameter(type,dic);
                case ParameterType.number:
                    return new NumberParameter(type, dic);
                case ParameterType.sentence:
                    return new SentenceParameter(type, dic);
                case ParameterType.dropdown:
                    return new DropdownParameter(type, dic);
                case ParameterType.color:
                    return new ColorParameter(type, dic);
                case ParameterType.nodes:
                    return new NodesParameter(type, dic);
            }

        }

        return new Parameter(ParameterType.none, dic);
    }
}
