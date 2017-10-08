using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnData : MonoBehaviour
{

    public void dataInit(string _d)
    {
        string[] d = _d.Split(',');

        ID = Int32.Parse(d[0]);
        spawnTime = float.Parse(d[2]);
        spawnIndex = Int32.Parse(d[1]);
        enemyType = Int32.Parse(d[0]);
        spawnDirection = Int32.Parse(d[0]);
    }

    public int ID;
    public float spawnTime;
    public int enemyType;
    public int spawnIndex;
    public int spawnDirection;

}
