using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnkoEffect : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public GameObject unkoEffectCanvas;
    public GameObject unkoEffect;

	// Update is called once per frame
	void Update () {


        var parent = this.transform;

        if (Input.GetKeyDown(KeyCode.A))
        {
            Instantiate(unkoEffect,transform.position,transform.rotation,parent);
        }


	}
}
