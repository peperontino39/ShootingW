using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GoriraJump : MonoBehaviour
{

    public Transform nawPosition;
    public Transform terget;
    float time = 3;
    [SerializeField]
    AnimationCurve curve;
    public float rotateDirecction  = -1; //-1時計回り1反時計回り

    [SerializeField]
    Sprite a;
    [SerializeField]
    Sprite b;
    [SerializeField]
    Sprite c;

    public float sa;
    public float sb;
    public float sc;

    IEnumerator Start()
    {

        GetComponent<SpriteRenderer>().sprite = a;
        transform.localScale = new Vector3(sa, sa, 0);
        transform.localPosition = nawPosition.position;
        yield return new WaitForSeconds(1f);
        transform.localScale = new Vector3(sb, sb, 0);
        GetComponent<SpriteRenderer>().sprite = b;
        jumpStart(()=> {

            transform.localScale = new Vector3(sc, sc, 0);
            GetComponent<SpriteRenderer>().sprite = c;
            transform.localRotation = Quaternion.identity;
        });

    }

    public void jumpStart(Action callback = null)
    {
        var r = 0;
        bool ttakai = nawPosition.position.y < terget.position.y;
        StartCoroutine(Easing.Tween(time, (t) =>
        {
            var x = Mathf.Lerp(nawPosition.position.x, terget.position.x, t);
            var y = ttakai ? nawPosition.position.y + (-nawPosition.position.y + terget.position.y) * curve.Evaluate(t) :
            terget.position.y + (-terget.position.y + nawPosition.position.y) * curve.Evaluate(1-t);

            transform.localPosition = new Vector3(x, y, Mathf.Lerp(nawPosition.position.z, terget.position.z, t));
            transform.localRotation = Quaternion.Euler(0, 0, (r += 10) * rotateDirecction);
        }, callback));
    }




}
