using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public enum SceneStane
{
    OPENING,
    MOVIE, 
    TUTORIAL,
    SPAWN,
    STAGE_ONE,
    STAGE_BOSS,
    CLEAR
}


public class DataManager : MonoBehaviour
{

    static private DataManager instans;
    static public DataManager Instans
    {
        get
        {
            if (instans == null)
            {
                var obj = Instantiate(new GameObject("DataManager"));
                var _instans = obj.AddComponent<DataManager>();
                instans = _instans;
            }
            return instans;
        }
    }



    public List<SpawnData> SpawnDatas = new List<SpawnData>();
    public List<EnemyStates> EnemyDatas = new List<EnemyStates>();
    public SceneStane sceneState = SceneStane.OPENING;


    void Start()
    {
    }
    void Awake()
    {
        instans = this;
        LoadStage("Stage1");
        LoadEnemyData();
    }

    void LoadStage(string fileName)
    {

        var sr = new StreamReader(Application.streamingAssetsPath + "/Data/Stage/" + fileName + ".csv");
        string text = sr.ReadLine();
        while ((text = sr.ReadLine()) != null)
        {
            if (text == "")
            {
                continue;
            }
            var sd = new SpawnData();
            sd.dataInit(text);
            SpawnDatas.Add(sd);
        }
    }

    public void LoadEnemyData()
    {
        var sr = new StreamReader(
            Application.streamingAssetsPath + "/Data/EnemyStatus.csv");
        string text = sr.ReadLine();
        while ((text = sr.ReadLine()) != null)
        {
            if (text == "")
            {
                continue;
            }
            EnemyStates ed = new EnemyStates();
            ed.DataInit(text);
            EnemyDatas.Add(ed);
        }
    }


}
