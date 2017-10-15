using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    int MaxHp = 100;

    int HP;
    [SerializeField]
    Slider bar;
    [SerializeField]
    ShakePosition shakepos;
    int killEnemyNum; //倒した数

    public void addKillEnemyNum() {
        killEnemyNum++;
    }


    void Start()
    {
        HP = MaxHp;
        bar.maxValue = MaxHp;
        killEnemyNum = 0;
    }

    public void Damage(int _damage)
    {
        HP -= _damage;
        bar.value = HP;
        shakepos.Shake();

    }

}
