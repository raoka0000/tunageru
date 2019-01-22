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
        if(isOpen == false && BoxViewModel.instance.baseBox != null){
            if(BoxViewModel.instance.baseBox.config.type == BoxConfig.ExportType.websocket){
                this.WebsocketOutput();
            }else{
                this.window.InEffect();
                this.OutputPath = BoxViewModel.instance.baseBox.config.path;
            }
        }
    }

    public void HideWiondow(){
        if(isOpen == true){
            this.window.OutEffect();
        }
    }

    public void Output(){
        BaseNode baseNode = MainViweModel.instance.baseNode;
        BaseBox baseBox = BoxViewModel.instance.baseBox;
        ConfirmViewModel.instance.ShowWindow(() =>{
            TunagumIO.ExportBaseBox(baseBox);
            this.HideWiondow();
        }, null, "出力しますか?", "");   
    }

    public void WebsocketOutput(){
        ConfirmViewModel.instance.ShowWindow(() =>{
            this.HideWiondow();
            this.HideWiondow();
        }, null, "データを送信しますか?", "");   

    }

}
