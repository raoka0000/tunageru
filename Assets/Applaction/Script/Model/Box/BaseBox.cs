using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UniRx;

public class BaseBox : Box{
    public string optionText;
    public BoxConfig config;

    public ReactiveCollection<Box> boxes = new ReactiveCollection<Box>();
    public List<string> programIdList = new List<string>();

    //public List<string> palette = new List<string>();
    //public Dictionary<string,string> palette = new Dictionary<string, string>();//<マテリアル名,カテゴリー名>

    public struct Palett{
        public string boxtag;
        public string category;
        public Palett(string boxtag, string category){
            this.boxtag   = boxtag;
            this.category = category;
        }
    }

    public List<Palett> palette = new List<Palett>();

    List<Box> _templateBoxes = null;
    List<Box> templateBoxes{
        get{
            if(_templateBoxes == null){
                this.SetTemplate();
            }
            return this._templateBoxes;
        }
    }

    public static BaseBox ConvertOptionText(string optionText){
        string boxText = optionText.Between("[<base>]", "[/<base>]");
        return new BaseBox(boxText, optionText);
    }


    private BaseBox(string boxText, string optionText):base(boxText){
        this.optionText = optionText;
        string conf = optionText.Between("[<config>]","[/<config>]");
        this.config = new BoxConfig(conf);
        this.tag = "@<base>@";
        this.baseBox = this;

        this.baseBox.boxes
            .ObserveRemove()
            .Subscribe(x => RemovedEvent(x.Value));


        this.SetPalette();
    }

    private void SetTemplate(){
        string[] nodeTexts = optionText.Betweenes("[<box>]", "[/<box>]");
        List<Box> boxlist = new List<Box>();
        foreach (string nodeText in nodeTexts){
            var b = new Box(nodeText);
            boxlist.Add(b);
        }
        this._templateBoxes = boxlist;
    }

    public Box SearchTemplateBox(string tag){
        foreach(Box b in templateBoxes){
            if(b.tag == tag){
                return b;
            }
        }
        return null;
    }


    private void SetPalette(){
        string[] paletteTexts = optionText.Betweenes("[<palette>]", "[/<palette>]");
        foreach(string paletteText in paletteTexts){
            string categoryText = paletteText.Between("[<title>]", "[/<title>]");
            string[] materialTexts = paletteText.Betweenes("[<material>]", "[/<material>]");
            foreach (string str in materialTexts){
                palette.Add(new Palett(str, categoryText));
            }
        }
    }


    public Box AddBoxForTag(string tag){
        foreach(Box box in templateBoxes){
            if(box.tag == tag){
                var b = box.Clone();
                this.boxes.Add(b);
                RefreshProgramID();
                b.baseBox = this;
                return b;
            }
        }
        return null;
    }

    public void RemovedEvent(Box box){
        foreach(Box b in box.parentBox){
            b.RemoveChildBox(box);
        }
        foreach(Box b in box.childBox){
            box.RemoveChildBox(b);
        }
    }


    private void SetBoxIdAndGroupId(){
        int gid = 0;
        var query = boxes.GroupBy(b => b.programId);
        foreach (var group in query){
            int newid = 0;
            foreach (Box b in group){
                b.id = newid;
                b.groupId = gid;
                newid += 1;
            }
            gid += 1;
        }
    }

    public string NewProgramID(){
        String GUID = Guid.NewGuid().ToString("N");
        programIdList.Add(GUID);
        return GUID;
    }

    public string IntegrationProgramID(string id1, string id2){
        string newguid = this.NewProgramID();
        programIdList.Remove(id1);
        programIdList.Remove(id2);
        foreach(Box b in boxes){
            if(b.programId == id1 || b.programId == id2){
                b.programId = newguid;
            }
        }
        return newguid;
    }

    public void RefreshProgramID(){
        foreach(Box b in boxes){
            b.programId = NewProgramID();
        }
        foreach(Box b in this.childBox){
            SetProgramID(b);
        }

    }

    public void SetProgramID(Box box){
        foreach (Box cb in box.childBox){
            if (cb.programId != box.programId){
                this.IntegrationProgramID(cb.programId, box.programId);
            }
            SetProgramID(cb);
        }
    }

    private void ReConnection(){
        foreach(Box b in boxes){
            if(b.parentBox.Count == 0 && !(b is BaseBox)){
                this.AddChildBox(b);
            }
        }
    }

    protected override string GetValue(){
        ReConnection();
        RefreshProgramID();
        SetBoxIdAndGroupId();

        string v = base.GetValue();


        v = Regex.Replace(v, "\n{1,}", "\n");
        for (int i = this.childBox.Count - 1; i >= 0; i-- ){
            this.RemoveChildBox(this.childBox[i]);
        }

        return v;
    }


}
