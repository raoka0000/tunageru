using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class DragAttribute : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [System.NonSerialized]
    public RectTransform myRect;
    [System.NonSerialized]
    public bool isDraging = false;
    
    // Use this for initialization
    void Start()
    {
        myRect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDraging = true;
    }


    public void OnDrag(PointerEventData eventData)
    {
        myRect.position += new Vector3(eventData.delta.x, eventData.delta.y, 0f);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDraging = false;
    }
}
