using System.Collections;
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

    public enum AnimationState
    {
        NORMAL = 0,     //通常
        SMILE = 1,      //笑顔
        CRY = 3,        //泣く
        ANGER = 2,      //怒り
        SUFFERING = 4,  //苦しむ
        WALK = 6,       //歩き
    }
    AnimationState animationState;
    SpriteRenderer sprite;

    static Sprite[] sprites;
    [SerializeField]
    Player player;
    public EnemyState state;
    public bool isLive = true;      //生きてるかどうか


    public void DataInit(string _d)
    {
        state.DataInit(_d);
    }

    public void AddDamage(int _damage)
    {
        state.hitPoint -= _damage;
        if (state.hitPoint <= 0)
        {
            StartCoroutine(Suffring(() =>
            {
                isLive = false;

                Destroy(gameObject);
            }));
        }
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

    }
    public void faceCange(AnimationState ani)
    {
        if (state.hitPoint > 0)
        {
            animationState = ani;
            sprite.sprite = sprites[(int)ani];
        }
        StartCoroutine(Easing.Deyray(1, () =>
        {
            if (state.hitPoint > 0)
            {
                animationState = AnimationState.ANGER;
                sprite.sprite = sprites[(int)animationState];
                StartCoroutine(Easing.Deyray(1, () =>
                {
                    faceCange(AnimationState.SMILE);
                }));
                player.Damage(state.atackPower);
            }

        }));

    }

    public IEnumerator Motion(AnimationState ani, Action callback = null)
    {
        animationState = ani;
        sprite.sprite = sprites[(int)ani];
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
        if (animationState == AnimationState.SUFFERING)
        {
            yield break;
        }
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
        if (callback != null)
        {
            callback();
        }
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
