using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumingAnime : MonoBehaviour {
    [SerializeField]
    GameObject image;
    [SerializeField]
    GameObject image2;

    float count =0;

    bool isActive = false;

	// Use this for initialization
	void Start () {

        StartCoroutine(druming());
    }
	
	// Update is called once per frame
	void Update () {     

      
    }

    IEnumerator druming()
    {
        while (true)
        {

            image.SetActive(true);
            image2.SetActive(false);

            yield return new WaitForSeconds(0.5f);

            image.SetActive(false);
            image2.SetActive(true);

            yield return new WaitForSeconds(0.5f);

        }     

    }

}
