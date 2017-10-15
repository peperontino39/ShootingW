using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//集中線を動かすクラス
public class ConcentrationLine : MonoBehaviour
{


    Vector3 defaultSize;
    float deltaTime = 0;
    void Start()
    {

        defaultSize = transform.localScale;

    }

    void Update()
    {

        var s = Mathf.Sin(deltaTime * 20);
        transform.localScale = defaultSize + new Vector3(s, s, 0);
        deltaTime += Time.deltaTime;
    }
}
