using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ThrowUnko : MonoBehaviour {


    public Transform target;
    float time = 3;
    [SerializeField]
    AnimationCurve curve;
    float rotateDirection = -1; //-1時計回り1反時計回り
    public Action callback = null;
    void Start()
    {
        target = GameObject.Find("Target").transform;
        throwStart();
    }

    void throwStart()
    {
        var r = 0;
        bool ttakai = transform.position.y < target.position.y;
        StartCoroutine(Easing.Tween(time, (t) =>
        {
            var x = Mathf.Lerp(transform.position.x, target.position.x, t);
            var y = ttakai ? transform.position.y + (-transform.position.y + target.position.y) * curve.Evaluate(t) :
            target.position.y + (-target.position.y + transform.position.y) * curve.Evaluate(1 - t);

            transform.localPosition = new Vector3(x, y, Mathf.Lerp(transform.position.z, target.position.z, t));
            transform.localRotation = Quaternion.Euler(0, 0, (r += 10) * rotateDirection);
        }, callback));
      
    }
}
