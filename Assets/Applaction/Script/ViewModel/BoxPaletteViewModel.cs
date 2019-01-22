using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPaletteViewModel : SingletonMonoBehaviour<BoxPaletteViewModel> {
    public GameObject baseList;
    public GameObject noCategorylist;
    public BoxPaletteMaterial materialPrefab;
    public BoxPaletteCategory categoryPrefab;
    public Dictionary<string, BoxPaletteCategory> categoryTable = new Dictionary<string, BoxPaletteCategory>();
    public GameObject DorpPanel;
    private GameObject currentDraggObj;

    public void LoadPalette(BaseBox bb){
        foreach(BaseBox.Palett p in bb.palette){
            var tBox = bb.SearchTemplateBox(p.boxtag);
            if(p.category.IsBlank()){
                AddMaterial(tBox);
            }else{
                if(!categoryTable.ContainsKey(p.category)){
                    AddCategory(p.category);
                }
                AddMaterial(tBox, categoryTable[p.category]);
            }
        }
    }

    public void AddMaterial(Box box){
        BoxPaletteMaterial.Instantiate(materialPrefab.gameObject, noCategorylist, box);
    }
    public void AddMaterial(Box box, BoxPaletteCategory category){
        category.AddMaterial(materialPrefab.gameObject, box);
    }
    public void AddCategory(string name){
        var obj = BoxPaletteCategory.Instantiate(categoryPrefab.gameObject, baseList.transform, name);
        categoryTable.Add(name, obj);
        noCategorylist.transform.SetAsLastSibling();
    }

    public GameObject GetDraggingObject(string tag){
        Box tBox = BoxViewModel.instance.baseBox.SearchTemplateBox(tag);
        GameObject obj = BoxPaletteMaterial.Instantiate(materialPrefab.gameObject, DorpPanel, tBox).gameObject;
        currentDraggObj = obj;
        return obj;
    }

    /*public void OnDragMaterial(BoxPaletteMaterial material){
        
    }

    public void OnDropDummy(){
        
    }*/

    public void PutBox(BoxPaletteMaterial material){
        BoxViewModel.instance.AddBoxForTag(material.tagName);
    }

    public void Dorp(){
        if (currentDraggObj == null) return; 
        var mat = currentDraggObj.GetComponent<BoxPaletteMaterial>();
        PutBox(mat);
    }

}
