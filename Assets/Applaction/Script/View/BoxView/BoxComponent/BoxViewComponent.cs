using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxViewComponent<T> : MonoBehaviour where T : BoxViewComponent<T> {
    public Text title;
    public Text dscription;
    public GameObject dscriptionObj;
    public Content content;


    public static T Instantiate(GameObject prefab, GameObject parent, Content content){
        T obj = Instantiate(prefab, parent.transform).GetComponent<T>();
        obj.init(content);
        return obj;
    }

    public virtual void init(Content content){
        this.content = content;
        if (content.title == null){
            this.title.gameObject.SetActive(false);
        }else{
            this.title.text = content.title;
        }
        if (content.desc == null){
            this.dscriptionObj.SetActive(false);
        }else{
            this.dscription.text = content.desc;
        }

        return;
    }
}
