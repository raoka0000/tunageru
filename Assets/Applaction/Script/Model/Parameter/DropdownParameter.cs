using System.Collections;
using System.Collections.Generic;

public class DropdownParameter : Parameter{
    public List<string> list = new List<string>();

    public DropdownParameter(ParameterType type, Dictionary<string, string> dic) : base(type, dic){
        if (dic.ContainsKey("list")){
            foreach(string str in dic["list"].Split(',')){
                this.list.Add(str);
            } 
        } 
    }

}
