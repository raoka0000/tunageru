using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BoxPaletteCategory : MonoBehaviour {
    public GameObject materialList;
    public RectTransform ArrowIcon;
    public Text title;

    public static BoxPaletteCategory Instantiate(GameObject prefab, Transform parent, string title){
        BoxPaletteCategory obj = Instantiate(prefab, parent).GetComponent<BoxPaletteCategory>();
        obj.SetTitle(title);
        return obj;
    }

    public void SwitchOpenColse(){
        if(materialList.activeSelf){
            //クローズ処理
            materialList.SetActive(false);
            ArrowIcon.DORotate(new Vector3(0, 0, 90), 0.1f).SetEase(Ease.InOutBack);
        }else{
            materialList.SetActive(true);
            ArrowIcon.DORotate(new Vector3(0, 0, -90), 0.1f).SetEase(Ease.InOutBack);
        }
    }

    public void AddMaterial(GameObject prefab, Box box){
        BoxPaletteMaterial.Instantiate(prefab, materialList, box);
    }

    public void SetTitle(string title){
        this.title.text = title;
    }
}
