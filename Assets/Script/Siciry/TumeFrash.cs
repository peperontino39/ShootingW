using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TumeFrash : MonoBehaviour {

    [SerializeField]
    GameObject maskWall;
    [SerializeField]
    GameObject tumeImage;

    public float speed = 30.0f;

    float yPos;
    float yPos2;


    bool isSlide = false;
    // Use this for initialization
    void Start () {
        yPos = maskWall.transform.position.y;
        yPos2 = tumeImage.transform.position.y;
    }
	
	// Update is called once per frame
	void Update () {
        maskWall.transform.position = new Vector3(transform.localPosition.x,yPos,transform.localPosition.z);

        tumeImage.transform.position = new Vector3(tumeImage.transform.position.x, yPos2, tumeImage.transform.position.z);

        slide();

	}

    void slide()
    {
        if (yPos > -450 )
        {
            yPos -= speed;
            
        }
        if (yPos <= -450)
        {
            Destroy(this.gameObject);
        }

        
    }
}
