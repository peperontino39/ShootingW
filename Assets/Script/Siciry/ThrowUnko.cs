using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ThrowUnko : MonoBehaviour {

    //public Camera _camera;
    public Vector3 target;
    float time = 1;
    [SerializeField]
    AnimationCurve curve;
    float rotateDirection = -1; //-1時計回り1反時計回り
    public Action callback = null;
    public GameObject unkoEffectCanvas;
    public GameObject unkoEffect;

    void Start()
    {
        
        target = GameObject.Find("Target").transform.position ;
        unkoEffectCanvas = GameObject.Find("EffectCanvas");
        throwStart();
       
    }

    void throwStart()
    {
        
        var r = 0;
        var pos = transform.position;
        var parent = unkoEffectCanvas.transform;

        StartCoroutine(Easing.Tween(time, (t) =>
        {
            transform.localRotation = Quaternion.Euler(0, 0, (r += 10) * rotateDirection);
            transform.position = Vector3.Lerp(pos, target,t);
            
        }, ()=> {

            if (callback != null)
            {
                callback();
            }
            target.z = 10.0f;

            Vector3 screenPos = Camera.main.WorldToViewportPoint(target);

            Vector2 WorldObject_ScreenPosition = new Vector2(
            //((screenPos.x * parent.GetComponent<RectTransform>().sizeDelta.x) - (parent.GetComponent<RectTransform>().sizeDelta.x * 0.5f)),
            //((screenPos.y * parent.GetComponent<RectTransform>().sizeDelta.y) - (parent.GetComponent<RectTransform>().sizeDelta.y * 0.5f)));
           );
            //var vaf_x = screenPos.x * parent.GetComponent<RectTransform>().sizeDelta.x;
            //var vaf_y = screenPos.x * parent.GetComponent<RectTransform>().sizeDelta.y;
            var vaf_x = 800.0f;
            var vaf_y = 450.0f;
          //  screenPos.x = parent.GetComponent<RectTransform>().sizeDelta.x;
            parent.GetComponent<RectTransform>().anchoredPosition = WorldObject_ScreenPosition;

            Debug.Log(target);
            Debug.Log(screenPos);
           var obj = Instantiate(unkoEffect, WorldObject_ScreenPosition, Quaternion.Euler(0.0f,0.0f,0.0f),parent);
            Debug.Log("x" + vaf_x);
            Debug.Log("y" + vaf_y);
            obj.transform.localPosition = new Vector3(screenPos.x * vaf_x * 1.1f - vaf_x/2.0f,screenPos.y * vaf_y * 1.1f - vaf_y/2.0f ,0);

            Destroy(gameObject);

        }));
      
    }
}
