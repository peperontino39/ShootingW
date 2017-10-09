using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakePosition : MonoBehaviour
{


    public AnimationCurve curve;

    Vector3 pos;
    void Start()
    {
        pos = transform.position;
        
    }
  
    public void Shake()
    {
        StartCoroutine(Easing.Tween(0.2f, (t) =>
        {
            float x = Mathf.Lerp(pos.x + 1, pos.x - 1, curve.Evaluate(t) * 2);
            float y = Mathf.Lerp(pos.y + 1, pos.y - 1, curve.Evaluate(t) * 2);

            transform.position = new Vector3(x, y, pos.z);
            
        }, () =>
        {
            transform.position = pos;
        }));
    }


}
