using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    static private DataManager instans = null;
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

    [SerializeField]
    AudioSource seAudioSource;

    [SerializeField]
    AudioSource bgmAudioSource;

    void Start () {
        var getIns = Instans;
    }
	
	
}
