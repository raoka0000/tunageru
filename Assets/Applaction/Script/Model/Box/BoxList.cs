using System.Collections;
using System.Collections.Generic;

public class BoxList{
    public string optionText { private set; get; }

    public Box baseBox;
    private List<Box> _templateBoxes = null;
    public List<Box> templateBoxes{
        get{
            if (_templateBoxes == null){
                this.SetTemplate();
            }
            return _templateBoxes;
        }
    }


    public BoxList(string optionText){
        this.optionText = optionText;
        string baseNodeText = optionText.Between("[<base>]", "[/<base>]");
        this.baseBox = new Box(baseNodeText);
        this.baseBox.tag = "base";

    }


    private void SetTemplate(){
        string[] nodeTexts = optionText.Betweenes("[<box>]", "[/<box>]");
        List<Box> boxlist = new List<Box>();
        foreach (string nodeText in nodeTexts){
            boxlist.Add(new Box(nodeText));
        }
        this._templateBoxes = boxlist;

    }





}
