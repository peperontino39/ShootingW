using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Natumitest : MonoBehaviour {

    [SerializeField]
    Sprite a;
    [SerializeField]
    Sprite b;
    [SerializeField]
    Sprite c;

    [SerializeField]
    GameObject pre;


    IEnumerator Start () {

        yield return new WaitForSeconds(1f);

        GetComponent<SpriteRenderer>().sprite = a;

        yield return new WaitForSeconds(1f);
        transform.localRotation = Quaternion.Euler(0, 0, 0); yield return new WaitForSeconds(1f);

        float time = 0.2f;
        yield return new WaitForSeconds(time);
        transform.localRotation = Quaternion.Euler(0, 0, -10);
        yield return new WaitForSeconds(time);
        transform.localRotation = Quaternion.Euler(0, 0, 10);
        yield return new WaitForSeconds(time);
        transform.localRotation = Quaternion.Euler(0, 0, -10);
        yield return new WaitForSeconds(time);
        transform.localRotation = Quaternion.Euler(0, 0, 10);




    }


}
