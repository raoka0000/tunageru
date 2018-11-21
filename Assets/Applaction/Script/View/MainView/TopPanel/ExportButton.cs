using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExportButton : MonoBehaviour {
    public void Pushed(){
        Export.Output(BaseNode.activeBaseNode.Value);
    }
}
