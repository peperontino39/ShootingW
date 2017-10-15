using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ThrowUnko : MonoBehaviour {

    public Vector3 nawPosition;
    public Transform target;
    float time = 3;
    [SerializeField]
    AnimationCurve curve;
    float rotateDirection = -1; //-1時計回り1反時計回り

    void Start()
    {
        nawPosition = GameObject.Find("player").transform.position;
        target = GameObject.Find("Target").transform;
        throwStart();

    }

    void throwStart(Action callback = null)
    {
        var r = 0;
        bool ttakai = nawPosition.y < target.position.y;
        StartCoroutine(Easing.Tween(time, (t) =>
        {
            var x = Mathf.Lerp(nawPosition.x, target.position.x, t);
            var y = ttakai ? nawPosition.y + (-nawPosition.y + target.position.y) * curve.Evaluate(t) :
            target.position.y + (-target.position.y + nawPosition.y) * curve.Evaluate(1 - t);

            transform.localPosition = new Vector3(x, y, Mathf.Lerp(nawPosition.z, target.position.z, t));
            transform.localRotation = Quaternion.Euler(0, 0, (r += 10) * rotateDirection);
        }, callback));
    }
}
