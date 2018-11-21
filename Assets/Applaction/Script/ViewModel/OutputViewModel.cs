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

    public void ShowWindow(){
        if(isOpen == false && MainViweModel.instance.baseNode != null){
            this.window.InEffect();
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
            Export.OutputFile(baseNode.Value, this.OutputPath, baseNode.outputFileName);
            this.HideWiondow();
        }, null, "出力しますか?", this.OutputPath +"/" + baseNode.outputFileName + "\nに出力します");
        
    }

}
