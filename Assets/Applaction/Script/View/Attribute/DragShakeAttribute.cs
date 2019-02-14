using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class DragShakeAttribute : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    [System.NonSerialized]
    public RectTransform myRect;
    [System.NonSerialized]
    public bool isDraging = false;

    public UnityEvent shakeEvent;

    private float shakeTimeLimit= 1.0f;
    private float shakeLowerTimeLimit = 0.05f;
    private int shakeThreshold = 8;

    private Vector3 acceleration;
    private Vector3 preAcceleration;
    private float dotProduct;
    private int shakeCount;
    private float lastShakeTime;
    private float lastShakeEndTime=0;

    // Use this for initialization
    void Start(){
        myRect = GetComponent<RectTransform>();
    }


    public void OnBeginDrag(PointerEventData eventData){
        isDraging = true;
        shakeCount = 0;
    }


    public void OnDrag(PointerEventData eventData){
        var delta = new Vector3(eventData.delta.x, eventData.delta.y, 0f);
        myRect.position += delta;

        preAcceleration = acceleration;
        acceleration = delta;
        dotProduct = Vector3.Dot(acceleration, preAcceleration);
        if (dotProduct < 0){
            if (Time.time - lastShakeEndTime > shakeLowerTimeLimit){
                if (Time.time - lastShakeTime < shakeTimeLimit){
                    shakeCount++;
                    lastShakeEndTime = Time.time;
                }else{
                    shakeCount = 0;
                }
            }
        }
        if(shakeCount > shakeThreshold){
            shakeEvent.Invoke();
        }
        lastShakeTime = Time.time;

    }

    public void OnEndDrag(PointerEventData eventData){
        isDraging = false;
    }

}
