using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour {

    bool isDead = false;

    [SerializeField]
    GameObject face;
    [SerializeField]
    GameObject dead;
   


    float time;
    [SerializeField]
    float time_max;

    Fade fade;

    // Use this for initialization
    void Start()
    {
        dead.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDead = true;
        }
        if (isDead == true)
        {
            dead.SetActive(true);
            face.SetActive(false);
            time += Time.deltaTime;
        }
        if (time >= time_max)
        {
            Fade.Instance.isFadeOut = true;
        }
        if (time >= 4.0f)
        {
            SceneManager.LoadScene("Stage1");
        }
    }
}
