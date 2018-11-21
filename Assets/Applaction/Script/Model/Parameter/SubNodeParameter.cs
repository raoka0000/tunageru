using System.Collections;
using System.Collections.Generic;

public class SubNodeParameter : Parameter{
    private SubNode _subNode;
    public SubNode subNode{
        get{
            if(this._subNode == null){
                this._subNode = findSubNode(this.Tag).Clone();
            }
            return this._subNode;
        }
        set{
            this._subNode = value;
        }
    }

    public bool optional = false;
    public bool canOutput = true;

    public SubNodeParameter(ParameterType type, Dictionary<string, string> dic):base(type, dic){
        if (dic.ContainsKey("optional")) this.optional = (dic["optional"] == "true");
    }

    private SubNode findSubNode(string tag){
        foreach(SubNode sb in BaseNode.activeBaseNode.nodes){
            if (sb.tag == tag) return sb;
        }
        return null;
    }

    protected override string GetValue(){
        if (!canOutput) return "";
        return subNode.Value;
    }
}
