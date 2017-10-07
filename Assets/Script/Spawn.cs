using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Spawn : MonoBehaviour
{

    public GameObject Enemy;
    public GameObject[] SpawnPoint;

    void Start()
    {


    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            EnemySpawn(0);
        }
        if (Input.GetMouseButtonDown(1))
        {
            EnemySpawn(1);
        }
        if (Input.GetMouseButtonDown(2))
        {
            EnemySpawn(2);
        }
    }

    void EnemySpawn(int spawnID)
    {

        var ene = Instantiate(Enemy);
        ene.transform.position = SpawnPoint[spawnID].transform.position;

        float ti = 0.5f;
        StartCoroutine(Tween(ti, (t) =>
        {
            ene.transform.position = Vector3.Lerp(
                SpawnPoint[spawnID].transform.position,
                 SpawnPoint[spawnID].transform.position + new Vector3(-1, 0, 0), t);
        }, () =>
        {
            StartCoroutine(Tween(ti, (t) =>
            {
                ene.transform.position = Vector3.Lerp(
                    SpawnPoint[spawnID].transform.position + new Vector3(-1, 0, 0),
                     SpawnPoint[spawnID].transform.position, t);
            },()=> {
                Destroy(ene);
            }));
        }));
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
