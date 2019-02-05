using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class BoxViewModel : SingletonMonoBehaviour<BoxViewModel> {

    private Color positiveColor = new Color(0.504717f, 0.6309656f, 1f);
    private Color negativeColor = new Color(0.5f, 0.5f, 0.5f);

    [System.NonSerialized]
    public BaseBox baseBox;

    public BoxWindow boxWindowPrefab;
    public GameObject windowViewObject;

    public BoxNumberComponent numberComponent;
    public BoxTextComponent textComponent;
    public BoxRadioButtonConponent raidoComponent;
    public BoxToggleComponent toggleComponent;


    [System.NonSerialized]
    public List<BoxWindow> boxWindows = new List<BoxWindow>();
    [System.NonSerialized]
    public Box currentConnectBox;
    [System.NonSerialized]
    public mode currentMode = mode.nomal;
    public enum mode{
        nomal,
        connectAll,
        connectPrent,
        connectchild
    }




    public void OpenNewBaseNode(){
        if (this.baseBox == null) return;
        this.AddWindow((Box)this.baseBox);

        foreach(Box b in this.baseBox.boxes){
            AddWindow(b);
        }
    }


    public void SetBaseBox(BaseBox box){
        this.baseBox = box;
        this.baseBox.boxes
            .ObserveAdd()
            .Subscribe( x => AddWindow(x.Value));

        this.baseBox.boxes
            .ObserveRemove()
            .Subscribe(x => RemoveWindow(x.Value));

        OpenNewBaseNode();
        BoxPaletteViewModel.instance.LoadPalette(this.baseBox);
    }

    private BoxWindow AddWindow(Box box){
        if (box.invisible) return null;
        var obj = BoxWindow.Instantiate(boxWindowPrefab.gameObject, windowViewObject, box);

        if(box.parentBox.Count > 0){
            BoxWindow parentWindow = SearchBoxWindow(box.parentBox[0]);
            if(parentWindow != null){
                RectTransform objTrans = obj.GetComponent<RectTransform>();
                RectTransform prentTrans = parentWindow.GetComponent<RectTransform>();
                int a = parentWindow.box.childBox.Count;
                int b = (a % 2 == 0) ? -1 : 1;
                int x = a / 2 * b;
                objTrans.anchoredPosition = new Vector2(prentTrans.anchoredPosition.x + x * 300, prentTrans.anchoredPosition.y - prentTrans.sizeDelta.y - 300);
            }
        }

        boxWindows.Add(obj);

        return obj;
    }

    private void RemoveWindow(Box box){
        var win = SearchBoxWindow(box);
        boxWindows.Remove(win);
        win.RemoveAnime();
    }

    public void AddBox(Box box){
        this.baseBox.boxes.Add(box);
    }

    public void AddBoxForTag(string tag){
        this.baseBox.AddBoxForTag(tag);
    }

    public void RemoveBox(Box box){
        this.baseBox.boxes.Remove(box);
    }

    public BoxWindow SearchBoxWindow(Box box){
        foreach(var b in this.boxWindows){
            if(b.box == box){
                return b;
            }
        }
        return null;
    }

    public void GoConnectMode(Box box, mode mode){
        currentConnectBox = box;
        currentMode = mode;
        if(mode == mode.connectPrent){
            foreach (var bw in this.boxWindows){
                if (bw.box == box) continue;
                if (bw.box.programId != box.programId && bw.box.adaptBoxTags.Contains(box.tag)){
                    bw.SetColor(positiveColor);
                    bw.connectModeFlag = true;
                }else{
                    bw.SetColor(negativeColor);
                    bw.connectModeFlag = false;
                }
            }
        }else if(mode == mode.connectchild){
            foreach (var bw in this.boxWindows){
                if (bw.box == box) continue;
                if (bw.box.programId != box.programId && box.adaptBoxTags.Contains(bw.box.tag)){
                    bw.SetColor(positiveColor);
                    bw.connectModeFlag = true;
                }else{
                    bw.SetColor(negativeColor);
                    bw.connectModeFlag = false;
                }
            }
        }
    }

    public void ConnectDrop(Box box){
        if (currentConnectBox == box) return;
        if(currentMode == mode.connectPrent){
            if(box.adaptBoxTags.Contains(currentConnectBox.tag)){
                box.AddChildBox(currentConnectBox);
            }
        }else if(currentMode == mode.connectchild){
            if(currentConnectBox.adaptBoxTags.Contains(box.tag)){
                currentConnectBox.AddChildBox(box);
            }
        }
    }

    public void Connect(Box abox, Box bbox){
        if (abox.adaptBoxTags.Contains(bbox.tag)){
            abox.AddChildBox(bbox);
        }else if (bbox.adaptBoxTags.Contains(abox.tag)){
            bbox.AddChildBox(abox);
        }
    }

    public void GoNomalMode(){
        currentConnectBox = null;
        currentMode = mode.nomal;
        foreach (var bw in this.boxWindows){
            bw.ResetColor();
        }

    }
}
