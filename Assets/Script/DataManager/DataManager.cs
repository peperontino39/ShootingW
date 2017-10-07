using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class DataManager : MonoBehaviour {

    static private DataManager instans = null;
    static public DataManager Instans {
        get {
            if(instans ==null)
            {
                var obj = Instantiate(new GameObject("DataManager"));
                var _instans = obj.AddComponent<DataManager>();
                instans = _instans;
            }
            return instans; }
    }

    public List<SpawnData> SpawnData;

    void Start()
    {
        LoadStage("Stage1");
    }

    void LoadStage(string fileName)
    {
        var sr = new StreamReader(Application.streamingAssetsPath + "/Data/" + fileName + ".csv");
        string text = "";
        while ((text = sr.ReadLine()) != null)
        {
            Debug.Log(text);
            //AddAttackAreas(text.Split(','));
        }
    }
}
