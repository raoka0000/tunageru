using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ZoomAttribute : MonoBehaviour, IScrollHandler,IBeginDragHandler,IEndDragHandler,IDragHandler{
    [System.NonSerialized]
    public RectTransform myRect;

    [SerializeField, HeaderAttribute("0の場合は制限なし")]
    float max = 0;
    [SerializeField]
    float min = 0;

    //ドラッグしている指のデータ
    private Dictionary<int, PointerEventData> _draggingDataDict = new Dictionary<int, PointerEventData>();
    //ピンチ開始時の指の距離
    private float _beforeDistanceOfPinch;


    void Start(){
        myRect = GetComponent<RectTransform>();

    }


    public void OnScroll(PointerEventData eventData){
        float scale = myRect.localScale.y + eventData.scrollDelta.y * 0.01f;
        if (max > 0) scale = (scale > max) ? max : scale;
        if (min > 0) scale = (scale < min) ? min : scale;
        myRect.localScale = new Vector3(scale, scale, 1.0f);
    }


    public void OnBeginDrag(PointerEventData eventData){
        //ドラッグデータ追加
        _draggingDataDict[eventData.pointerId] = eventData;
    }

    public void OnEndDrag(PointerEventData eventData){
        //ドラッグデータ削除
        if (_draggingDataDict.ContainsKey(eventData.pointerId)){
            _draggingDataDict.Remove(eventData.pointerId);
        }
    }

    public void OnDrag(PointerEventData eventData){
        //ドラッグデータ更新
        _draggingDataDict[eventData.pointerId] = eventData;

        //2本以上ドラッグデータがある時はピンチ
        if (_draggingDataDict.Count >= 2){
            OnPinch();
        }
    }


    private void OnPinch(){

        //最初と2本目のタッチ情報取得
        Vector2 firstTouch = Vector2.zero, secondTouch = Vector2.zero;
        int count = 0;

        foreach (var draggingData in _draggingDataDict){
            if (count == 0){
                firstTouch = draggingData.Value.position;
                count = 1;
            }
            else if (count == 1){
                secondTouch = draggingData.Value.position;
                break;
            }
        }

        //ピンチの幅を取得
        float distanceOfPinch = Vector2.Distance(firstTouch, secondTouch);

        //現在地の座標差も算出し、座標差からピンチの倍率を計算
        float pinchiDiff = distanceOfPinch - _beforeDistanceOfPinch;

        _beforeDistanceOfPinch = distanceOfPinch;

        float scale = myRect.localScale.y + pinchiDiff * 0.01f;
        if (max > 0) scale = (scale > max) ? max : scale;
        if (min > 0) scale = (scale < min) ? min : scale;
        myRect.localScale = new Vector3(scale, scale, 1.0f);

    }


}
