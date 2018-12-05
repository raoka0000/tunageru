using System.IO;
using UnityEngine;
using WebSocketSharp;

public class Export{

    static private WebSocket ws;

    static private string outputFilePath = DEFINE.OutputFilePath;

    static public void Output(string txt){
        string filePath = DEFINE.OutputFilePath + "/" + DEFINE.OutputFileName;
        StreamWriter sw = new StreamWriter(filePath, false); //true=追記 false=上書き
        sw.WriteLine(txt);
        sw.Flush();
        sw.Close();
    }

    static public void OutputFile(string txt, string path, string fileName){
        if (fileName == "") fileName = "NoNameText.txt";
        string filePath = path + "/" + fileName;
        StreamWriter sw = new StreamWriter(filePath, false); //true=追記 false=上書き
        sw.WriteLine(txt);
        sw.Flush();
        sw.Close();
    }

    static public void BaseNode(BaseNode bn){
        switch(bn.exportConfig.type){
            case ExportConfig.ExportType.file:
                Export.OutputFile(bn.Value,bn.exportConfig.path,bn.exportConfig.fileName);
                break;
            case ExportConfig.ExportType.websocket:
                WebSocketPost(bn);
                break;
            default:
                break;
        }
    }


    static public void WebSocketPost(BaseNode bn){
        ConnectWebSocket.instance._serverAddress = bn.exportConfig.path;
        ConnectWebSocket.instance._port = bn.exportConfig.port;
        ConnectWebSocket.instance.Post(bn.Value);
    }


    /*static public void StartWebSocket(string address, string port){
        var ca = "ws://" + address + ":" + port;
        Debug.Log("Connect to " + ca);
        ws = new WebSocket(ca);

        //Add Events
        //On catch message event
        ws.OnMessage += (object sender, MessageEventArgs e) => {
            Debug.Log(e.Data);
        };

        //On error event
        ws.OnError += (sender, e) => {
            Debug.Log("WebSocket Error Message: " + e.Message);
        };

        //On WebSocket close event
        ws.OnClose += (sender, e) => {
            Debug.Log("Disconnected Server");
        };

        ws.Connect();
    }

    static public void StopWebSocket(){
        ws.Close();
    }*/


}
