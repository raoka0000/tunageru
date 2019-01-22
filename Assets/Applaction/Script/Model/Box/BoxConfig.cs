using System.Collections;
using System.Collections.Generic;

public class BoxConfig{
    public ExportType type { get { return _type; } }
    ExportType _type = ExportType.file;
    public enum ExportType{
        none,
        file,
        websocket
    }

    public string configText;
    public string path;

    private string _fileName = "no_name.txt";
    public string fileName { get { return _fileName; } set { _fileName = value; } }

    public string port { get; set; }

    public BoxConfig(string configText){
        this.configText = configText;
        string str = configText.Between("[<exportType>]", "[/<exportType>]").Trim();
        switch (str){
            case "file":
                _type = ExportType.file;
                fileName = configText.Between("[<name>]", "[/<name>]").Trim();
                break;
            case "websocket":
                _type = ExportType.websocket;
                port = configText.Between("[<port>]", "[/<port>]").Trim();
                break;
            case "none":
                _type = ExportType.none;
                break;
            default:
                _type = ExportType.file;
                break;
        }
        this.path = configText.Between("[<path>]", "[/<path>]").Trim();
        if (this.path.IsPresent()){
            this.path = DEFINE.OutputFilePath;
        }

    }

}
