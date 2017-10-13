using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public struct EnemyStates
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

public enum AnimationState
{
    NORMAL = 0,     //通常
    SMILE = 1,      //笑顔
    CRY = 3,        //泣く
    ANGER = 2,      //怒り
    SUFFERING = 4,  //苦しむ
    WALK = 6,       //歩き
}


public class EnemyBase : MonoBehaviour
{
    AnimationState animationState;
    SpriteRenderer sprite;

    Sprite[] walkSprites;
    Sprite[] waitSprites;
    Sprite[] atackSprites;
    Sprite[] damageSprites;

    [SerializeField]
    Player player;

    public EnemyStates states;
    public bool isLive = true;      //生きてるかどうか


    public void DataInit(string _d)
    {
        states.DataInit(_d);
        SpriteInit();
    }

    //エネミーにダメージを与える関数
    public void AddDamage(int _damage)
    {
        states.hitPoint -= _damage;
        if (states.hitPoint <= 0)
        {
            StartCoroutine(Suffring(() =>
            {
                isLive = false;
                Destroy(gameObject);
            }));
        }
    }


    //歩く
    virtual public IEnumerator Waik()
    {
        yield return null;
    }


    //ダメージ食らったとき
    virtual public IEnumerator Damage(Action callback = null)
    {
        yield return null;
    }


    //死ぬ
    virtual public IEnumerator Die(Action callback = null)
    {
        float time = 0.5f;

        for (int i = 0; i < damageSprites.Length; i++)
        {
            sprite.sprite = damageSprites[i];
            yield return new WaitForSeconds(time);
        }
        if (callback != null)
        {
            callback();
        }
    }
    //攻撃モーション
    virtual public IEnumerator Atack()
    {
        sprite.sprite = atackSprites[0];
        yield return null;
    }
    //歩き
    virtual public IEnumerator Walk()
    {
        animationState = AnimationState.WALK;
        float time = 0.2f;

        for (int i = 0; true; i++)
        {
            sprite.sprite = walkSprites[i % walkSprites.Length];
            yield return new WaitForSeconds(time);
        }
    }
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(Walk());
    }

    public void SpriteInit()
    {
        walkSprites = Resources.LoadAll<Sprite>("NotShare/素材/gh/" + states.nameing + "/Walk");
        waitSprites = Resources.LoadAll<Sprite>("NotShare/素材/gh/" + states.nameing + "/Wait");
        atackSprites = Resources.LoadAll<Sprite>("NotShare/素材/gh/" + states.nameing + "/Atack");
        damageSprites = Resources.LoadAll<Sprite>("NotShare/素材/gh/" + states.nameing + "/Damage");
    }
    

    public IEnumerator Motion(AnimationState ani, Action callback = null)
    {
        animationState = ani;
        sprite.sprite = waitSprites[(int)ani];
        yield return new WaitForSeconds(1.0f);
        if (callback != null)
        {
            callback();
        }
        StartCoroutine(Walk());
        //sprite.sprite = sprites[(int)AnimationState.NORMAL];
    }

    IEnumerator Suffring(Action callback = null)
    {
       yield return null;
    }






}
