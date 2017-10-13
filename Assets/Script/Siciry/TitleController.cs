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
   IEnumerator Start()
    {
        dead.SetActive(false);
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                break;
            }
            yield return null;
        }
        dead.SetActive(true);
        face.SetActive(false);

        yield return new WaitForSeconds(time_max);

        Fade.Instance.startFadeOut(()=> { SceneManager.LoadScene("Stage1"); });


    }
    

    // Update is called once per frame
    void Update()
    {
      
       
    }
}
