using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public static class StringExtensions{

    public static string Between(this string str, string a,string b){
        Regex regex = new Regex(@"(\" + a + @")(?<value>.+?)(\" + b + ")", RegexOptions.Singleline);
        return regex.Match(str).Groups["value"].Value;
    }

    public static string[] Betweenes(this string str, string a, string b){
        Regex regex = new Regex(@"(\" + a + @")(?<value>.+?)(\" + b + ")", RegexOptions.Singleline);
        var matchs = regex.Matches(str);
        string[] arrStr = new string[matchs.Count];

        for (int i = 0; i < matchs.Count; i++){
            arrStr[i] = matchs[i].Groups["value"].Value;
        }

        return arrStr;
    }


    public static string[] SplitLine(this string str){
        return str.Replace("\r\n", "\n").Split('\n');
    }

    public static string[] SplitSpace(this string str){
        return str.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
    }

    public static Color ToColor(this string str){
        var color = default(Color);
        if (!ColorUtility.TryParseHtmlString(str, out color)){
            Debug.LogWarning("Unknown color code... " + str);
        }
        return color;
    }

    public static bool IsPresent(this string str){
        return str.IsBlank();
    }

    public static bool IsBlank(this string str){
        return string.IsNullOrEmpty(str);
    }


}