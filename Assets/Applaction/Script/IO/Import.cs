using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

// ファイル読み込み
public class Import {

    static private string inputFilePath = DEFINE.InputFilePath;

    // 読み込み関数
    static public string ReadFile(string fileName){
        string text = "";
        // FileReadTest.txtファイルを読み込む
        FileInfo fi = new FileInfo(inputFilePath + "/" + fileName);
        try{
            // 一行毎読み込み
            using (StreamReader sr = new StreamReader(fi.OpenRead(), Encoding.UTF8)){
                text = sr.ReadToEnd();

            }
        }catch (Exception e){
            // 改行コード
            text = "";
        }
        return text;
    }

    static public BaseNode LoadBaseNode(){
        string optionText = ReadFile(DEFINE.OptionFileName);
        return BaseNode.ConvertOptionText(optionText);
    }

}