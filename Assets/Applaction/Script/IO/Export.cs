using System.IO;

public class Export{

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



}
