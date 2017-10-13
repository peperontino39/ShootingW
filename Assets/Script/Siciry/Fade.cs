using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class Fade : MonoBehaviour
{

    private static Fade instance;

    [SerializeField]
    Image fadeImage;                //透明度を変更するパネルのイメージ
    

    public static Fade Instance
    {
        get
        {

            if (instance == null)
            {
                var obj = Instantiate((GameObject)Resources.Load("Prefabs/fadeCanvas"));
                instance = obj.GetComponent<Fade>();
                instance.fadeImage = obj.transform.GetChild(0).GetComponent<Image>();

            }
            return instance;
        }
    }
    
    private void Awake()
    {
        if (instance == null) instance = this;
        DontDestroyOnLoad(gameObject);
    }



    void Start()
    {


    }

    public void startFadeIn(Action callback = null)
    {
        StartCoroutine(Easing.Tween(1.0f, (t) =>
        {
            fadeImage.color = new Color(0, 0, 0, 1 - t);
        }, callback));
    }

    public void startFadeOut(Action callback = null)
    {
        StartCoroutine(Easing.Tween(1.0f, (t) =>
        {
            fadeImage.color = new Color(0, 0, 0, t);
        }, callback));
    }

    void Update()
    {



    }


}
