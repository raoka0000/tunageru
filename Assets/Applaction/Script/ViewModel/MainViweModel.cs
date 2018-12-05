using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainViweModel : SingletonMonoBehaviour<MainViweModel>{

    [System.NonSerialized]
    public List<NodeWindow> nodeWindowList = new List<NodeWindow>();
    public BaseNode baseNode;
    public GameObject windowViewObject;

    //どうにかして
    public GameObject NodeWindowPrefab{ get { return nodeWindowPrefab.gameObject; }}
    public GameObject SubNodeListComponentPrefab { get { return subNodeListComponentPrefab.gameObject; } }
    public GameObject SubNodeListComponentUnitPrefab { get { return subNodeListComponentUnitPrefab.gameObject; } }
    public GameObject NumberComponentPrefab { get { return numberComponentPrefab.gameObject; } }
    public GameObject SubNodeListPlusPrefab{ get { return subNodeListPlusPrefab.gameObject; }}
    public GameObject SubNodeListPlusWiondowPrefab{ get { return subNodeListPlusWiondowPrefab.gameObject; }}
    public GameObject SubNodeListPlusUnitPrefab{ get { return subNodeListPlusUnitPrefab.gameObject; }}
    public GameObject SentenceComponentPrefab { get { return sentenceComponentPrefab.gameObject; } }
    public GameObject DropdownComponentPrefab { get { return dropdownComponentPrefab.gameObject; } }

    public NodeWindow nodeWindowPrefab;
    public SubNodeListComponent subNodeListComponentPrefab;
    public SubNodeListComponentUnit subNodeListComponentUnitPrefab;
    public SubNodeListPlus subNodeListPlusPrefab;
    public SubNodeListPlusWiondow subNodeListPlusWiondowPrefab;
    public SubNodeListPlusUnit subNodeListPlusUnitPrefab;
    public NumberComponent numberComponentPrefab;
    public SentenceComponent sentenceComponentPrefab;
    public DropdownComponent dropdownComponentPrefab;


    public void OpenNewBaseNode(BaseNode baseNode){
        this.baseNode = baseNode;
        this.AddWindow(this.baseNode);
    }

    public NodeWindow AddWindow(Node node, bool isOpen = true, NodeWindow parentWindow = null){
        var obj = NodeWindow.Instantiate(NodeWindowPrefab, windowViewObject, node);
        nodeWindowList.Add(obj);
        if(isOpen == false){
            //obj.Close();
        }
        //親ウインドウがある時は位置を調整する。
        if (parentWindow != null){
            obj.parentWindow = parentWindow;
            RectTransform objTrans = obj.GetComponent<RectTransform>();
            RectTransform prentTrans = parentWindow.GetComponent<RectTransform>();
            int a = obj.parentWindow.childWindows.Count;
            int b = (a % 2 == 0) ? -1:1;
            int x = a / 2 * b;
            objTrans.anchoredPosition = new Vector2(prentTrans.anchoredPosition.x + x * 600, prentTrans.anchoredPosition.y - prentTrans.sizeDelta.y - 150);
            LineViewModel.instance.AddLine(prentTrans, objTrans);
        }

        return obj;
    }

    public bool RemoveWindow(Node node){
        NodeWindow n = this.SearchWindow(node);
        if(n != null){
            if(n.parentWindow != null){
                n.parentWindow.childWindows.Remove(n);
            }
            nodeWindowList.Remove(n);
            Destroy(n.gameObject);
            return true;
        }
        return false;
    }

    public NodeWindow SearchWindow(Node node){
        foreach(NodeWindow nw in nodeWindowList){
            if (nw.node == node) return nw;
        }
        return null;
    }

    public void MoveToNodeWindow(Node node){
        foreach(NodeWindow nw in nodeWindowList){
            if(nw.node == node){
                this.MoveToNodeWindow(nw);
                return;
            }
        }
    }

    //調整中
    public void MoveToNodeWindow(NodeWindow window){
        RectTransform viewRect   = windowViewObject.gameObject.GetComponent<RectTransform>();
        RectTransform windowRect = window.gameObject.GetComponent<RectTransform>();
        //viewRect.DOAnchorPos(windowRect.anchoredPosition, 1.0f).SetEase(Ease.OutCirc);
        //viewRect.DOMove(windowRect.position, 1.0f).SetEase(Ease.OutCirc);
    }
}
