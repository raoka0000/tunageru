using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BoxPaletteMaterial : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler {
    private Color defaultColor = new Color(0.95f, 0.95f, 0.95f);

    public Image icon;
    public Text title;
    public string tagName;

    public GameObject draggingObject;

    public static BoxPaletteMaterial Instantiate(GameObject prefab, GameObject parent, Box box){
        BoxPaletteMaterial obj = Instantiate(prefab, parent.transform).GetComponent<BoxPaletteMaterial>();
        obj.icon.sprite = ImageIO.GetIcon(box.iconName);
        obj.title.text = box.title;
        obj.tagName = box.tag;
        Image bg = obj.GetComponent<Image>();
        if(box.color.IsPresent()){
            bg.color = box.color.ToColor();
        }else{
            bg.color = obj.defaultColor;
        }
        return obj;
    }

    public void OnPointerClick(PointerEventData pointerEventData){
        BoxPaletteViewModel.instance.PutBox(this);
    }

    public void OnPushed(){
        BoxPaletteViewModel.instance.PutBox(this);
    }

    public void OnBeginDrag(PointerEventData pointerEventData){
        draggingObject = BoxPaletteViewModel.instance.GetDraggingObject(this.tagName);
        draggingObject.transform.position = pointerEventData.position;
    }

    public void OnDrag(PointerEventData pointerEventData){
        draggingObject.transform.position = pointerEventData.position;
    }

    public void OnEndDrag(PointerEventData pointerEventData){
        //gameObject.GetComponent<Image>().color = Vector4.one;
        Destroy(draggingObject);
    }

    public void Drop(){
        
    }

}
