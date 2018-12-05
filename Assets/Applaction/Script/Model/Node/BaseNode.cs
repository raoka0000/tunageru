using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class BaseNode : Node{
    static public BaseNode activeBaseNode;

    public List<SubNode> _nodes;
    public List<SubNode> nodes{
        get{
            if(this._nodes == null){
                this.GetNodes();
            }
            return this._nodes;
        }
    }

    public ExportConfig exportConfig;

    public string optionText;

    static public BaseNode ConvertOptionText(string optionText){
        string baseNodeText = optionText.Between("[<base>]", "[/<base>]");
        string exportConfigText = optionText.Between("[<config>]", "[/<config>]");
        BaseNode nb = new BaseNode(optionText, baseNodeText, exportConfigText);
        return nb;
    }

    protected BaseNode(string optionText, string nodeText, string exportConfigText) : base(nodeText){
        this.exportConfig = new ExportConfig(exportConfigText);
        this.optionText = optionText;
        BaseNode.activeBaseNode = this;
    }

    protected void GetNodes(){
        string[] nodeTexts = this.optionText.Betweenes("[<sub>]", "[/<sub>]");
        this._nodes = new List<SubNode>();
        foreach (string s in nodeTexts){
            SubNode n = new SubNode(s);
            _nodes.Add(n);
        }
    }

    protected override string getValue(){
        string outText = base.getValue();
        foreach (SubNode n in nodes){
            outText = outText.Replace(n.tag, "");
        }
        return outText;
    }

}
