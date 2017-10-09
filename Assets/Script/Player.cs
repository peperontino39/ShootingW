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
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Damage(1);
        //}

    }

    public void Damage(int _damage)
    {
        HP -= _damage;
        bar.value = HP;
        shakepos.Shake();

    }

}
