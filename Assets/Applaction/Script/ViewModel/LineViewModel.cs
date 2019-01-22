using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LineViewModel : SingletonMonoBehaviour<LineViewModel>{
    public float lineSize = 2.0f;
    public List<MainViewLine> lines = new List<MainViewLine>();
    public GameObject lineView;
    public GameObject linePrefab;
    public GameObject dummyPrefab;

    public MainViewLine AddLine(GameObject parent, GameObject child){
        RectTransform a = parent.GetComponent<RectTransform>();
        RectTransform b = child.GetComponent<RectTransform>();
        return this.AddLine(a, b);
    }

    public MainViewLine AddLine(RectTransform parent, RectTransform child){
        MainViewLine line = MainViewLine.Instantiate(linePrefab, lineView, parent, child);
        this.lines.Add(line);
        return line;
    }

    public bool RemoveLine(MainViewLine line){
        var b = this.lines.Remove(line);
        if(b){
            Destroy(line.gameObject);
        }
        return b;
    }

    public bool RemoveLine(GameObject parent, GameObject child){
        RectTransform a = parent.GetComponent<RectTransform>();
        RectTransform b = child.GetComponent<RectTransform>();
        return this.RemoveLine(a, b);
    }

    public bool RemoveLine(RectTransform a, RectTransform b){
        foreach(MainViewLine line in lines){
            if((line.startRect == a && line.endRect == b) || (line.startRect == b && line.endRect == a)){
                return RemoveLine(line);
            }
        }
        return false;
    }

}
