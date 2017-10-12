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
        var sds = DataManager.Instans.SpawnDatas;
        for (int i = 0; i < spawnPoint.Length; i++)
        {
            spawnDatas.Add(new List<SpawnData>());
        }

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

    //敵がリスポーンし始めます
    public void StartEnemyGame()
    {
        for (int i = 0; i < spawnPoint.Length; i++)
        {
            StartCoroutine(StartEnemySpawn(i));
        }
    }

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



    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //EnemySpawn(0);
        }
        if (Input.GetMouseButtonDown(1))
        {
            //EnemySpawn(1);
        }
        if (Input.GetMouseButtonDown(2))
        {
            //EnemySpawn(2);
        }
    }

    GameObject EnemySpawn(SpawnData spawn)
    {
        var ene = Instantiate(enemy);
        var enecom = ene.GetComponent<Enemy>();
        enecom.state = DataManager.Instans.EnemyDatas[spawn.enemyType];
        ene.transform.position = spawnPoint[spawn.spawnIndex].transform.position;

        var targets = spawnPoint[spawn.spawnIndex].transform.GetChild(spawn.spawnDirection);
        float ti = 1f;
        StartCoroutine(Easing.Tween(ti, (t) =>
        {
            if (enecom.state.hitPoint <= 0) return;
        ene.transform.position = Vector3.Lerp(
            spawnPoint[spawn.spawnIndex].transform.position,
             targets.position,t);// + new Vector3(-1, 0, 0), t);
        }, () =>
        {
            enecom.faceCange(Enemy.AnimationState.SMILE);
            
        }));
        return ene;
    }

   
}
