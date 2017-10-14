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

    void Start()
    {
        HP = MaxHp;
        bar.maxValue = MaxHp;
    }
   

    public void Damage(int _damage)
    {
        HP -= _damage;
        bar.value = HP;
        shakepos.Shake();

    }

}
