using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;


abstract public class Node{
    public string title;
    public string iconName;
    public string text;
    public string data;
    public string Value { get { return getValue(); } }
    public List<Parameter> parameters;

    public Node(string nodeText){
        this.title = nodeText.Between("[<title>]", "[/<title>]").Trim();
        this.iconName = nodeText.Between("[<icon>]", "[/<icon>]").Trim();
        this.text = nodeText.Between("[<text>]", "[/<text>]").Trim('\n');
        string dataFileName = nodeText.Between("[<fdata>]", "[/<fdata>]").Trim();
        if (dataFileName != ""){
            this.data = Import.ReadFile(dataFileName);
        }else{
            this.data = nodeText.Between("[<data>]", "[/<data>]").Trim('\n');
        }
        string pramText = nodeText.Between("[<pram>]", "[/<pram>]").Trim('\n');
        this.parameters = ParameterCreator.GetParameters(pramText);
    }

    protected virtual string getValue(){
        string outText = this.data;
        foreach (Parameter p in parameters){
            outText = outText.Replace(p.Tag, p.Value);
        }
        return outText;
    }
}
