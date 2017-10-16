using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ThrowUnko : MonoBehaviour {


    public Vector3 target;
    float time = 1;
    [SerializeField]
    AnimationCurve curve;
    float rotateDirection = -1; //-1時計回り1反時計回り
    public Action callback = null;
    void Start()
    {
        target = GameObject.Find("Target").transform.position ;
        throwStart();
    }

    void throwStart()
    {
        var r = 0;
        bool ttakai = transform.position.y < target.y;
        StartCoroutine(Easing.Tween(time, (t) =>
        {
            var x = Mathf.Lerp(transform.position.x, target.x, t);
            //var y = ttakai ? transform.position.y + (-transform.position.y + target.y) * curve.Evaluate(t) :
            //target.y + (-target.y + transform.position.y) * curve.Evaluate(1 - t);
            //transform.localRotation = Quaternion.Euler(0, 0, (r += 10) * rotateDirection);
            var y = Mathf.Lerp(transform.position.y, target.y, t);
            transform.position = new Vector3(x, y, Mathf.Lerp(transform.position.z, target.z, t));
            
        }, ()=> {

            //if (callback != null)
            //{
            //    callback();
            //}
            Destroy(this.gameObject);
        }));
      
    }
}
