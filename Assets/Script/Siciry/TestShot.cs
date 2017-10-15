using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShot : MonoBehaviour {

    void Start()
    {

    }


    public GameObject unkoPrehub;

    public void ShotUnko()
    {

        //var parent = this.transform;
        var obj =
        Instantiate(unkoPrehub);
        obj.transform.localPosition = transform.localPosition;
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShotUnko();
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position += transform.right * 10.0f * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position -= transform.right * 10.0f * Time.deltaTime;
        }
    }

}
