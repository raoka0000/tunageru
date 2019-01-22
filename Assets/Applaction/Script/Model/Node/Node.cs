using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;


abstract public class Node{
    public string tag;
    public string title;
    public string iconName;
    public string text;
    public string data;
    public string color;
    public bool   optional = false;
    public string Value { get { return getValue(); } }
    public List<Parameter> parameters;


    public List<Node> parentNode = new List<Node>();
    public List<Node> childNode = new List<Node>();

    public string nodeText = "";

    public Node(string nodeText){
        this.tag      = nodeText.Between("[<tag>]", "[/<tag>]").Trim();
        this.title    = nodeText.Between("[<title>]", "[/<title>]").Trim();
        this.iconName = nodeText.Between("[<icon>]", "[/<icon>]").Trim();
        this.color    = nodeText.Between("[<color>]", "[/<color>]").Trim();
        this.text     = nodeText.Between("[<text>]", "[/<text>]").Trim('\n');
        this.optional = nodeText.Contains("[<optional>]");

        string dataFileName = nodeText.Between("[<fdata>]", "[/<fdata>]").Trim();
        if (dataFileName != ""){
            this.data = Import.ReadFile(dataFileName);
        }else{
            this.data = nodeText.Between("[<data>]", "[/<data>]").Trim('\n');
        }
        string pramText = nodeText.Between("[<pram>]", "[/<pram>]").Trim('\n');
        this.parameters = ParameterCreator.GetParameters(pramText);

        this.nodeText = nodeText;
    }

    protected virtual string getValue(){
        string outText = this.data;
        foreach (Parameter p in parameters){
            outText = outText.Replace(p.Tag, p.Value);
        }
        return outText;
    }
}
