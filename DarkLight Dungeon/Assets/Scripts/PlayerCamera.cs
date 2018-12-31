using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

    private float mouseRotation;

    private Vector3 prevPos;
    private Vector3 currentPos;

    private Quaternion currentRotation;
    private Quaternion prevRotation;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        mouseRotation = Input.GetAxis("Mouse Y");

        currentPos = transform.position;
        currentPos.y += mouseRotation;

        //rotate camera
        transform.Rotate(mouseRotation, 0, 0);
        
        currentRotation = transform.rotation;

        //y rotation checks
        if (currentRotation.x > .26f) //if rotated more than ~30 degrees above the horizontal axis
        {

            currentRotation = prevRotation;
            currentPos = prevPos;
        }
        else if(currentRotation.x < -.26f) //if rotated more than ~30 degrees below the horizontal axis
        {
            currentRotation = prevRotation;
            currentPos = prevPos;
        }
        
        transform.position =  currentPos;
        prevPos = currentPos;

        transform.rotation = currentRotation;
        prevRotation = currentRotation;

    }
}
