using System;
using UnityEngine;

//シングルトン継承元クラス.
public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour{
	protected static T _instance;
	public static T instance {
		get {
			if (_instance == null) {
				_instance = FindObjectOfType<T> ();
				if (_instance == null) {
					//クラス名のゲームオブジェクトを生成する
                    _instance = new GameObject ("_").AddComponent<T> ();
				}
			}

			return _instance;
		}
	}

}