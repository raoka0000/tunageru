using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputViewModel : SingletonMonoBehaviour<OutputViewModel>{
    public OutputWindow window;
    public bool isOpen{
        get{
            return window.isOpen;
        }
    }
    [System.NonSerialized]
    public string OutputPath = DEFINE.OutputFilePath;

	public void Start(){
        //this.OutputPath = MainViweModel.instance.baseNode.exportConfig.path;
	}

	public void ShowWindow(){
        if(isOpen == false && MainViweModel.instance.baseNode != null){
            this.window.InEffect();
            this.OutputPath = MainViweModel.instance.baseNode.exportConfig.path;
        }
    }

    public void HideWiondow(){
        if(isOpen == true){
            this.window.OutEffect();
        }
    }

    public void Output(){
        BaseNode baseNode = MainViweModel.instance.baseNode;
        ConfirmViewModel.instance.ShowWindow(() =>{
            Export.BaseNode(baseNode);
            this.HideWiondow();
        }, null, "出力しますか?", "");
        
    }

}
