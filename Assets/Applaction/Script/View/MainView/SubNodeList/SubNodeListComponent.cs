using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubNodeListComponent : NodeWindowComponent{
    private List<SubNodeParameter> parameters;
    [System.NonSerialized]
    public NodeWindow prentWindow;

    public List<SubNodeListComponentUnit> unitList = new List<SubNodeListComponentUnit>();

    private SubNodeListPlus _subNodeListPlus;
    public SubNodeListPlus subNodeListPlus{
        get{
            if(this._subNodeListPlus == null){
                this._subNodeListPlus = SubNodeListPlus.Instantiate(MainViweModel.instance.SubNodeListPlusPrefab, this.gameObject, this);
            }
            return this._subNodeListPlus;
        }
    }

    public static SubNodeListComponent Instantiate(GameObject prefab, GameObject parent, List<SubNodeParameter> parameters, NodeWindow prentWindow){
        SubNodeListComponent obj = Instantiate(prefab, parent.transform).GetComponent<SubNodeListComponent>();
        obj.prentWindow = prentWindow;
        obj.parameters = parameters;
        obj.AddUnits(obj.parameters);
        return obj;
    }

    private void AddUnits(List<SubNodeParameter> parms){
        foreach(SubNodeParameter p in parms){
            if(p.optional){
                subNodeListPlus.AddUnit(p);
            }
            else{
                this.AddUnit(p);
            }
        }
    }

    public SubNodeListComponentUnit AddUnit(SubNodeParameter parameter){
        return SubNodeListComponentUnit.Instantiate(MainViweModel.instance.SubNodeListComponentUnitPrefab, this.gameObject, parameter);
    }

    public void ExpandNodeWindows(){
        foreach (SubNodeParameter p in this.parameters){
            MainViweModel.instance.AddWindow(p.subNode, false, prentWindow);
        }
    }

}