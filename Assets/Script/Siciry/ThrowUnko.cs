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
        var pos = transform.position;
        StartCoroutine(Easing.Tween(time, (t) =>
        {
          
            transform.position = Vector3.Lerp(pos, target,t);
            
        }, ()=> {

            //if (callback != null)
            //{
            //    callback();
            //}
            Destroy(gameObject);
        }));
      
    }
}
