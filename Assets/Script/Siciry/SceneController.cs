using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    //[SerializeField]
    //float 

    float time;
    [SerializeField]
    float time_max;

	// Use this for initialization
	void Start () {
        //tutorialCanvas.SetActive(false);
        //startCanvas.SetActive(false);
        Fade.Instance.startFadeIn(()=> {
            StartCoroutine(tutorialSTAGE());
        });
      
    }

    [SerializeField]
    Spawn spawn;

	// Update is called once per frame
	void Update () {

     
    }

   

    public AnimationCurve cameraWork;


    IEnumerator tutorialSTAGE(Action callback = null)
    {



        yield return 

        StartCoroutine(Easing.Tween(0.5f, (t) => {

            hamada_camera.transform.localPosition = new Vector3(hamada_camera.transform.localPosition.x
    , cameraWork.Evaluate(t), hamada_camera.transform.localPosition.z);

        }));

        yield return

       StartCoroutine(Easing.Tween(0.5f, (t) => {

           hamada_camera.transform.localPosition = new Vector3(hamada_camera.transform.localPosition.x
   , cameraWork.Evaluate(1-t), hamada_camera.transform.localPosition.z);

       }));

        yield return

       StartCoroutine(Easing.Tween(0.5f, (t) => {

           hamada_camera.transform.localPosition = new Vector3(hamada_camera.transform.localPosition.x
   , cameraWork.Evaluate(t), hamada_camera.transform.localPosition.z);

       }));

        yield return

       StartCoroutine(Easing.Tween(0.5f, (t) => {

           hamada_camera.transform.localPosition = new Vector3(hamada_camera.transform.localPosition.x
   , cameraWork.Evaluate(1 - t), hamada_camera.transform.localPosition.z);

       }));

        yield return

       StartCoroutine(Easing.Tween(0.5f, (t) => {

           hamada_camera.transform.localPosition = new Vector3(hamada_camera.transform.localPosition.x
   , cameraWork.Evaluate(t), hamada_camera.transform.localPosition.z);

       }));

        yield return

       StartCoroutine(Easing.Tween(0.5f, (t) => {

           hamada_camera.transform.localPosition = new Vector3(hamada_camera.transform.localPosition.x
   , cameraWork.Evaluate(1 - t), hamada_camera.transform.localPosition.z);

       }));

        yield return new WaitForSeconds(time_max/2);

        var vaf = tutorialCanvas.transform.localPosition.x;

        yield return StartCoroutine(Easing.Tween(1.0f, (t) =>
        {
            var x = Mathf.Lerp(vaf, 0, t);
            tutorialCanvas.transform.localPosition = new Vector3(x, 0, 0);
        }));

        yield return new WaitForSeconds(time_max/2);

        yield return StartCoroutine(Easing.Tween(1.0f, (t) =>
        {
            var x = Mathf.Lerp(0, -vaf, t);
            tutorialCanvas.transform.localPosition = new Vector3(x, 0, 0);
        }));

        startCanvas.SetActive(true);

        yield return new WaitForSeconds(time_max);

        startCanvas.SetActive(false);

        spawn.StartEnemyGame();

      

    }

}
