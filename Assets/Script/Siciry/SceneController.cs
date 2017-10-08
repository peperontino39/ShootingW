using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour {

    [SerializeField]
    GameObject hamada;
    [SerializeField]
    float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        hamada.transform.localPosition += transform.right * speed * Time.deltaTime;
    }
}
