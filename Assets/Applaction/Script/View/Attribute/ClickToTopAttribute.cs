using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickToTopAttribute : MonoBehaviour, ISelectHandler{

    public void OnSelect(BaseEventData eventData){
        transform.SetAsLastSibling();//今のGameObjectの子供内での一番下に
    }
}
