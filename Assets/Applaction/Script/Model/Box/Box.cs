using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UniRx;
using UnityEngine;

public class Box {
    public string boxText;

    public int id = -1;
    public int groupId = -1;
    public string programId;
    public string tag;
    public string title;
    public string iconName;
    public string text;
    public string data;
    public string color;
    public bool optional = false;
    public bool invisible = false;
    public string value { get { return GetValue(); } }

    public List<Content> contents;
    private List<string> _adaptBoxesTags;
    public List<string> adaptBoxTags{
        get{
            if(_adaptBoxesTags == null){
                SetAdaptBoxTags();
            }
            return _adaptBoxesTags;
        }
    }

    //public BaseBox baseBox{ get { return GetBaseBox(); }}


    public BaseBox baseBox;
    public ReactiveCollection<Box> parentBox = new ReactiveCollection<Box>();
    public ReactiveCollection<Box> childBox = new ReactiveCollection<Box>();


    public Box(string boxText){
        this.tag        = boxText.Between("[<tag>]", "[/<tag>]").Trim();
        this.title      = boxText.Between("[<title>]", "[/<title>]").Trim();
        this.iconName   = boxText.Between("[<icon>]", "[/<icon>]").Trim();
        this.color      = boxText.Between("[<color>]", "[/<color>]").Trim();
        this.text       = boxText.Between("[<desc>]", "[/<desc>]").Trim('\n');
        this.optional   = boxText.Contains("[<optional>]");
        this.invisible  = boxText.Contains("[<invisible>]");

        string dataFileName = boxText.Between("[<fdata>]", "[/<fdata>]").Trim();
        if (dataFileName != ""){
            this.data = Import.ReadFile(dataFileName);
        }
        else{
            this.data = boxText.Between("[<data>]", "[/<data>]").Trim('\n');
        }

        //
        string pramText = boxText.Between("[<contents>]", "[/<contents>]").Trim('\n');
        this.contents = ContentsGenerator.ContentsList(this, pramText);
        this.boxText = boxText;

    }

    public Box Clone(){
        return new Box(this.boxText);
    }


    protected virtual string GetValue(){
        string outText = this.data;
        foreach (Content c in this.contents){
            outText = outText.Replace(c.tag, c.value);
            //Debug.Log(c.tag + " : " + c.value + " : " + c.type);
        }

        return outText;
    }


    public bool AddChildBox(Box child){
        if (this.childBox.Contains(child)) return false;
        if (this.programId == child.programId) return false;
        this.childBox.Add(child);
        child.parentBox.Add(this);
        baseBox.RefreshProgramID();
        /*if(child.programId != this.programId){
            child.groupId = this.groupId;
        }*/
        //this.SetChildBoxId(this.id);
        return true;
    }

    public bool RemoveChildBox(Box child){
        foreach(Box b in this.childBox){
            if(b == child){
                this.childBox.Remove(b);
                child.parentBox.Remove(this);
                baseBox.RefreshProgramID();
                return true;
            }
        }
        return false;
    }


    public bool IsExistInHierarchy(Box box){
        foreach(Box pb in box.parentBox){
            if(pb == box){
                return true;
            }
            return pb.IsExistInHierarchy(box);
        }
        return false;
    }


    public void SetChildBoxId(int i){
        this.id = i;
        i += 1;
        foreach (Box b in childBox){
            b.id = i;
            b.SetChildBoxId(i);
            i += 1;
            //b.groupId = this.groupId;
        }
    }


    public void CheckParents(){
        
    }

    private void SetAdaptBoxTags(){
        this._adaptBoxesTags = new List<string>();
        foreach(var c in this.contents){
            if(c is ContentBoxes){
                var cBoxes = c as ContentBoxes;
                foreach(string ctag in cBoxes.list){
                    this._adaptBoxesTags.Add(ctag);
                }
            }
        }
    }

}
