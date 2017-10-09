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
        spawnIndex = Int32.Parse(d[1]);
        spawnTime = float.Parse(d[2]);
        movetime = float.Parse(d[3]);
        enemyType = Int32.Parse(d[4]);
        spawnDirection = Int32.Parse(d[5]);
    }

    public int ID;
    public float spawnTime;
    public float movetime;
    public int enemyType;
    public int spawnIndex;
    public int spawnDirection;

}
