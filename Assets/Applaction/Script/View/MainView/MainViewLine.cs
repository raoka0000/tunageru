using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainViewLine : MonoBehaviour{
    public RectTransform lineRect;
    public RectTransform startRect;
    public RectTransform endRect;

    public static MainViewLine Instantiate(GameObject prefab, GameObject parent, RectTransform start, RectTransform end){
        MainViewLine obj = Instantiate(prefab, parent.transform).GetComponent<MainViewLine>();
        obj.startRect = start;
        obj.endRect = end;
        return obj;
    }


    // Use this for initialization
    void Start(){
        lineRect = GetComponent<RectTransform>();
    }

    //無駄多いのでのちに修正
    private void OnGUI(){
        if(startRect == null || endRect==null){
            LineViewModel.instance.RemoveLine(this);
            return;
        }
        lineRect.localPosition = startRect.localPosition;
        float a = endRect.localPosition.x - startRect.localPosition.x;
        float b = endRect.localPosition.y - startRect.localPosition.y;
        float c = Mathf.Sqrt(a * a + b * b);
        float radian = Mathf.Atan2(b, a);

        lineRect.sizeDelta = new Vector2(c, 2);
        lineRect.localRotation = Quaternion.Euler(0, 0, radian * Mathf.Rad2Deg);
    }

}
