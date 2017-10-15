using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ゴリラ登場クラス
public class GoriraAppearance : MonoBehaviour {

    ShakePosition pos;
	void Start () {
		
	}
	
	void Update () {
        AppearanceStart();
    }

    public void AppearanceStart()
    {
        StartCoroutine(Appearance());
    }


    IEnumerator Appearance()
    {
        yield return null;
    }

}
