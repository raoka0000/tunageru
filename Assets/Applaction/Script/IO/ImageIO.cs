using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageIO {

    static public Sprite GetIcon(string name){
        var path = DEFINE.IconPath + "/" + name;
        var sprite = Resources.Load<Sprite>(path);
        return sprite;
    }
}
