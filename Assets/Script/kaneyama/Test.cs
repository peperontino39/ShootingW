using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {


    [SerializeField]
    Sprite a;
    [SerializeField]
    Sprite b;

    [SerializeField]
    GameObject pre;



    IEnumerator Start () {
        GetComponent<SpriteRenderer>().sprite = a;

        yield return new WaitForSeconds(1);
        GetComponent<SpriteRenderer>().sprite = b;

        Instantiate(pre, transform.GetChild(0));

    }
	

}
