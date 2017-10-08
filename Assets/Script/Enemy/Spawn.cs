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
            var ene = sdi[i];
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
        var enecom = 
            ene.GetComponent<Enemy>();
        enecom.state = DataManager.Instans.EnemyDatas[0];
        ene.transform.position = spawnPoint[spawn.spawnIndex].transform.position;

      
        float ti = 0.5f;
        StartCoroutine(Tween(ti, (t) =>
        {
            ene.transform.position = Vector3.Lerp(
                spawnPoint[spawn.spawnIndex].transform.position,
                 spawnPoint[spawn.spawnIndex].transform.position + new Vector3(-1, 0, 0), t);
        }, () =>
        {
            StartCoroutine(Tween(ti, (t) =>
            {
                ene.transform.position = Vector3.Lerp(
                    spawnPoint[spawn.spawnIndex].transform.position + new Vector3(-1, 0, 0),
                     spawnPoint[spawn.spawnIndex].transform.position, t);
            }, () =>
            {
                enecom.isLive = false;
                Destroy(ene);
            }));
        }));
        return ene;
    }
    IEnumerator Tween(float time, Action<float> call, Action callback = null)
    {
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            call(t / time);
            yield return null;
        }
        call(1.0f);
        if (callback != null)
            callback();
    }

}
