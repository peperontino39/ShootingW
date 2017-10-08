﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public struct EnemyState
{
    public int ID;                         //ID
    public string nameing;                 //名前
    public int atackPower;                 //攻撃力
    public int hitPoint;                   //Hp
    public float deylayTime;               //何秒後に打つか
    public float moveTime;                 //移動時間
    public int ReactionID;                 //リアクションID
    public void DataInit(string _d)
    {
        string[] d = _d.Split(',');
        ID = Int32.Parse(d[0]);
        nameing = d[1];
        atackPower = Int32.Parse(d[2]);
        hitPoint = Int32.Parse(d[3]);
        deylayTime = float.Parse(d[4]);
        moveTime = float.Parse(d[5]);
        ReactionID = Int32.Parse(d[6]);
    }
}

public class Enemy : MonoBehaviour
{

    enum AnimationState
    {
        NORMAL = 0,     //通常
        SMILE = 1,      //笑顔
        CRY = 2,        //泣く
        ANGER = 3,      //怒り
        SUFFERING = 4,  //苦しむ
        WALK = 6,       //歩き
    }
    AnimationState animationState;
    SpriteRenderer sprite;

    static Sprite[] sprites;

    public EnemyState state;
    public bool isLive = true;      //生きてるかどうか
    
    public Enemy(Enemy ene)
    {
        
    }
    public void DataInit(string _d)
    {
        state.DataInit(_d);

        //string[] d = _d.Split(',');
        //state.ID = Int32.Parse(d[0]);
        //state.nameing = d[1];
        //state.atackPower = Int32.Parse(d[2]);
        //state.hitPoint = Int32.Parse(d[3]);
        //state.deylayTime = float.Parse(d[4]);
        //state.moveTime = float.Parse(d[5]);
        //state.ReactionID = Int32.Parse(d[6]);

    }
    void Start()
    {
        if (sprites == null)
            sprites = Resources.LoadAll<Sprite>("NotShare/素材");

        sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(Walk());
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            StartCoroutine(Motion(AnimationState.ANGER));
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartCoroutine(Motion(AnimationState.CRY));
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StartCoroutine(Motion(AnimationState.SMILE));
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            StartCoroutine(Motion(AnimationState.NORMAL));
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            StartCoroutine(Suffring());
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {

        }
    }
    IEnumerator Motion(AnimationState ani)
    {
        animationState = ani;
        sprite.sprite = sprites[(int)ani];
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(Walk());
        //sprite.sprite = sprites[(int)AnimationState.NORMAL];
    }
    IEnumerator Suffring()
    {
        animationState = AnimationState.SUFFERING;
        float time = 0.5f;
        int i = 0;
        i = 4;
        sprite.sprite = sprites[i];
        yield return new WaitForSeconds(time);
        i = 5;
        sprite.sprite = sprites[i];
        yield return new WaitForSeconds(time);
        StartCoroutine(Walk());
    }
    IEnumerator Walk()
    {
        animationState = AnimationState.WALK;
        float time = 0.2f;
        int i = 0;
        while (true)
        {
            i = 6;
            sprite.sprite = sprites[i];
            yield return new WaitForSeconds(time);
            if (animationState != AnimationState.WALK)
                yield break;


            i = 7;
            sprite.sprite = sprites[i];
            yield return new WaitForSeconds(time);
            if (animationState != AnimationState.WALK)
                yield break;
        }
    }



}
