using System.Collections;
using System.Xml.Linq;
using System.Collections.Generic;
using UnityEngine;

public class TunagumIO {
    const string xmlPath = "/Users/raoka0000/Desktop/tunageruIO/input/save.xml";

    public static BaseBox LoadBaseBox(){
        string optionText = Import.ReadFile(DEFINE.OptionFileName);
        var bb = BaseBox.ConvertOptionText(optionText);
        XElement root = TunagumIO.LoadFromPath();
        var programs = root.Elements("program");
        foreach(XElement prog in programs){
            var titles = prog.Descendants("object");
            if (titles != null)
            {
                Dictionary<string, string> idToNextid = new Dictionary<string, string>();
                List<Box> boxlist = new List<Box>();

                foreach (var title in titles)
                {
                    string v = title.Attribute("name").Value;
                    string boxid = title.Attribute("boxID").Value;
                    string nextid = title.Attribute("nextBox").Value;
                    idToNextid[boxid] = nextid;
                    if (v == "Time")
                    {
                        var cbox = bb.AddBoxForTag("@<time>@");
                        cbox.id = int.Parse(boxid);
                        boxlist.Add(cbox);
                    }
                    if (v == "screen")
                    {
                        var cbox = bb.AddBoxForTag("@<screen>@");
                        cbox.id = int.Parse(boxid);
                        boxlist.Add(cbox);
                    }
                    if (v == "DVD")
                    {
                        var cbox = bb.AddBoxForTag("@<DVD>@");
                        cbox.id = int.Parse(boxid);
                        boxlist.Add(cbox);
                    }
                    if (v == "door")
                    {
                        var cbox = bb.AddBoxForTag("@<door>@");
                        cbox.id = int.Parse(boxid);
                        boxlist.Add(cbox);
                    }

                }
                //nextboxをchildにする処理.
                foreach (var b in boxlist)
                {
                    if (idToNextid.ContainsKey(b.id.ToString()))
                    {
                        string[] arr = idToNextid[b.id.ToString()].Split(',');
                        foreach (var str in arr)
                        {
                            foreach (var b2 in boxlist)
                            {
                                if (b2.id.ToString() == str)
                                {
                                    b.AddChildBox(b2);
                                }
                            }
                        }
                    }
                    if (b.id == 0)
                    {
                        bb.AddChildBox(b);
                    }
                }
            }
        }

        bb.RefreshProgramID();
        return bb;
    }









    public static void ExportBaseBox(BaseBox bb){
        Export.Output(bb.value);
    }


    public static XElement LoadFromPath(){
        string i_path = xmlPath;
        try
        {
            XDocument xml = System.Xml.Linq.XDocument.Load(i_path);
            XElement root = xml.Element("system");
            //XElement root = xml.root;
            /*var x = xml.Element("system");
            Debug.Log(x);
            var y = x.Elements("program");
            foreach(XElement ele in y){
                Debug.Log(ele);
            }*/
            // xmlデータをログに表示するよ。
            //ShowData(root);

            // 名前を指定して検索する場合は、この関数を使おう！
            /*var titles = root.Descendants("object");
            if (titles != null)
            {
                foreach (var title in titles)
                {
                    string nameText = title.Name.LocalName;
                    string valueText = title.Value;
                    string v = title.Attribute("name").Value;
                    Debug.LogFormat("name:{0}, value:{1}", nameText, v);
                }
            }*/
            return root;
        }
        catch (System.Exception i_exception)
        {
            Debug.LogErrorFormat("エラー : {0}", i_exception);
        }
        return null;
    }


    public static void ShowData(System.Xml.Linq.XElement i_element){
        string nameText = i_element.Name.LocalName;
        string valueText = i_element.Value;
        string atrText = string.Empty;

        // アトリビュートがある場合。
        if (i_element.HasAttributes)
        {
            foreach (var attribute in i_element.Attributes())
            {
                atrText += string.Format("\t\t{0}={1}\n", attribute.Name, attribute.Value);
            }
        }

        Debug.LogFormat("name:{0}\n\tvalue:{1}\n\tattribute:\n{2}", nameText, valueText, atrText);

        // 子ノードも探そう。
        foreach (var child in i_element.Elements())
        {
            ShowData(child);
        }
    }

}
