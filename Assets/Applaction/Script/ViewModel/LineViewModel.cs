using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineViewModel : SingletonMonoBehaviour<LineViewModel>{
    public List<MainViewLine> lines = new List<MainViewLine>();
    public GameObject lineView;
    public GameObject linePrefab;

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

}
