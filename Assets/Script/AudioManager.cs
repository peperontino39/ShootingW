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

    //[SerializeField]
    //AudioSource seAudioSource;

    [SerializeField]
    AudioSource bgmAudioSource;

    private Dictionary<string, AudioClip> seAudioClips;


    //SEを鳴らす
    public void PlaySE(string se_name)
    {
        var audsou = gameObject.AddComponent<AudioSource>();
        audsou.clip = seAudioClips[se_name];
        audsou.Play();
        StartCoroutine(AudioSourceIns(audsou));
    }
    //BGMを鳴らす
    public void PlayBGM(string se_name)
    {
        bgmAudioSource.clip = Resources.Load<AudioClip>("Audio/BGM" + se_name);
        bgmAudioSource.Play();

    }



    //なり終わったら消す
    IEnumerator AudioSourceIns(AudioSource au)
    {
        while (au.isPlaying)
        {
            yield return null;
        }
        Destroy(au);
    }

    public void Load(string filename)
    {
        var audios = Resources.LoadAll<AudioClip>(filename);
        foreach (var a in audios)
        {
            Debug.Log("効果音  " + a.name);
            seAudioClips.Add(a.name, a);
        }
    }

    void Start () {
        var getIns = Instans;
        Load("Audio/SE");
    }
	
	
}
