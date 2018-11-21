using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SidelistViewModel : SingletonMonoBehaviour<SidelistViewModel>{
    private List<BaseNode> baseNodes = new List<BaseNode>();

    public GameObject list;
    public GameObject contentPrefab;


    public void AddBaseNode(BaseNode baseNode){
        this.baseNodes.Add(baseNode);
        BaseNodeListButton.Instantiate(contentPrefab, list, baseNode);
    }
}
