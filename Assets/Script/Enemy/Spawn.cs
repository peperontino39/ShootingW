using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Spawn : MonoBehaviour
{

    public GameObject enemy;
    public GameObject[] spawnPoint;

    public List<List<SpawnData>> spawnDatas
        = new List<List<SpawnData>>();


    void Start()
    {
        //スポーンの数だけListを用意
        var sds = DataManager.Instans.SpawnDatas;
        for (int i = 0; i < spawnPoint.Length; i++)
        {
            spawnDatas.Add(new List<SpawnData>());
        }

        //スポーンごとにスポーンデータを用意
        for (int i = 0; i < sds.Count; i++)
        {
            for (int j = 0; j < spawnPoint.Length; j++)
            {
                if (sds[i].spawnIndex == j)
                {
                    var d = sds[i];
                    spawnDatas[j].Add(d);
                }
            }
        }

    }

    //全ての敵がリスポーンし始めます
    //ゲーム開始時にコールします
    public void StartEnemyGame()
    {
        for (int i = 0; i < spawnPoint.Length; i++)
        {
            StartCoroutine(StartEnemySpawn(i));
        }
    }

    //そのスポーン位置から出撃し始めます
    IEnumerator StartEnemySpawn(int index)
    {
        var sdi = spawnDatas[index];
        for (int i = 0; i < sdi.Count; i++)
        {
            SpawnData ene = sdi[i];
            yield return new WaitForSeconds(ene.spawnTime);

            var enedata = EnemySpawn(ene).GetComponent<Enemy>();

            while (enedata.isLive)
            {
                yield return null;
            }
        }
    }
    
    //スポーンデータを使って敵を出現させます
    GameObject EnemySpawn(SpawnData spawn)
    {
        var ene = Instantiate(enemy);
        EnemyBase enecom;

        
        enecom = ene.AddComponent<Enemy>();

        enecom.states = DataManager.Instans.EnemyDatas[spawn.enemyType];
        enecom.DataInit();
        //ene.transform.position = spawnPoint[spawn.spawnIndex].transform.position;
        var targets = spawnPoint[spawn.spawnIndex].transform.GetChild(spawn.spawnDirection);
        
        enecom.SpawnMove(spawnPoint[spawn.spawnIndex], targets.gameObject);

        return ene;
    }

   
}
