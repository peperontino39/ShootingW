using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noboriTest : MonoBehaviour
{

    [SerializeField]
    Sprite a;
    [SerializeField]
    Sprite b;
    [SerializeField]
    Sprite c;


    IEnumerator Start()
    {
        var pos = new Vector3(transform.localPosition.x, -13.7f, 0);
        var bi = true;
       StartCoroutine( Easing.Tween(3, (t) =>
        {
            transform.localPosition = Vector3.Lerp(
                new Vector3(transform.localPosition.x, pos.y, 0),
                new Vector3(transform.localPosition.x, 0, 0),
                t);
        },()=> {
            transform.localPosition = new Vector3(0, 0, 0);
            bi = false;
        }));

        while (bi)
        {

            GetComponent<SpriteRenderer>().sprite = a;
            yield return new WaitForSeconds(0.3f);
            GetComponent<SpriteRenderer>().sprite = b;
            yield return new WaitForSeconds(0.3f);

        }
        GetComponent<SpriteRenderer>().sprite = c;
        transform.localScale = new Vector3(1.2f, 1.2f, 0);

        yield return null;
    }
}
