using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour {


    [SerializeField]
    GameObject hamada_camera;
    [SerializeField]
    float hamada_speed;
    [SerializeField]
    float hamadaStopPos;
    [SerializeField]
    float hamadaDefPos;
    [SerializeField]
    float tutorialStopPos;
    [SerializeField]
    float tutorialOutPos;
    [SerializeField]
    float tutorial_speed;
    [SerializeField]
    GameObject tutorialCanvas;
    [SerializeField]
    GameObject startCanvas;


    float time;
    [SerializeField]
    float time_max;

	// Use this for initialization
	void Start () {
        tutorialCanvas.SetActive(false);
        startCanvas.SetActive(false);
        Fade.Instance.isFadeIn = true;
        Fade.Instance.isFadeOut = false;
    }
	
	// Update is called once per frame
	void Update () {
        
        if (hamada_camera.transform.localPosition.z < hamadaStopPos)
        {
            hamada_camera.transform.localPosition += transform.forward * hamada_speed * Time.deltaTime;
            cameraWork();
        }
        else
        {
            hamada_camera.transform.localPosition = new Vector3(hamada_camera.transform.localPosition.x,0.3f, hamada_camera.transform.localPosition.z);
        }
       

        if (hamada_camera.transform.localPosition.z >= hamadaStopPos)
        {
            //Debug.Log("とまった");
            tutorialCanvas.SetActive(true);
            if (tutorialCanvas.transform.localPosition.x < tutorialStopPos) {
                tutorialCanvas.transform.localPosition += transform.right * tutorial_speed * Time.deltaTime;
            }
            time += Time.deltaTime;
        }
        if (time >= time_max )
        {
            if (tutorialCanvas.transform.localPosition.x < tutorialOutPos)
            {
                tutorialCanvas.transform.localPosition += transform.right * (tutorial_speed * 4) * Time.deltaTime;
            }
            if (tutorialCanvas.transform.localPosition.x >= tutorialOutPos)
            {
                startCanvas.SetActive(true);
            }
        }
       

    }

    void cameraWork()
    {
        hamada_camera.transform.localPosition = new Vector3(hamada_camera.transform.localPosition.x
   , (Mathf.Cos((Time.frameCount * 1.5f) * 0.1f) + 1) * 0.5f, hamada_camera.transform.localPosition.z);
       

    }

}
