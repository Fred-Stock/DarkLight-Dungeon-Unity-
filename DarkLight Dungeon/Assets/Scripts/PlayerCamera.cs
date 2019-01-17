using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

    private float mouseRotation;
    private Vector3 playerDirection;

    private Vector3 prevPos;
    private Vector3 currentPos;
    private Vector3 prevPlayerPos;

    private Quaternion currentRotation;
    public Quaternion prevRotation;

    public GameObject player;

    private float percentage;
    private float prevPercentage;

    [SerializeField]
    private float distFromPlayer;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {

        //just need to fix the y rotation
        //look at overrides the rotations and maybe position? not sure what is exactly causing the bugs


        transform.LookAt(player.transform.position); //this breaks some things

        playerDirection = new Vector3(transform.forward.x, 0, transform.forward.z);
        playerDirection += player.transform.position;
        player.transform.LookAt(playerDirection);
        //Debug.Log(transform.forward);



        mouseRotation = Input.GetAxis("Mouse X");

        //////////////////////////////////// X rotations ///////////////////////////////////
        //rotate around the player like dark souls
        //not sure how to go about it yet
        //possibly find porportion of screen changed between last mouse pos and current mouse pos
        //use that to rotate around player
        
        //1. find current mouseRoation using get axis and divide by 1
        //2. use that to rotate camera by equivalent unit circle
        //to rotate around circle you must place it along a circle where the player is the center
        //turn camera to face player
        // can then multiply that by a scalar to implement a sensitivity thingy
        
        //redeclare variables when it works
        percentage = mouseRotation / 1f;
        
        percentage = Mathf.Atan(percentage);
        percentage *= .1f;
        percentage += prevPercentage;

        
        Vector3 pos = new Vector3(Mathf.Cos(-percentage), 0, Mathf.Sin(-percentage));
        pos = pos * distFromPlayer;

        pos = new Vector3(pos.x + player.transform.position.x, transform.position.y, pos.z + player.transform.position.z);
        
        //transform.position = pos;
        
        prevPercentage = percentage;


        /////////////////////////////////////// Y ROTATIONS ////////////////////////////////
        mouseRotation = Input.GetAxis("Mouse Y");

        //y rotation checks
        if (currentRotation.x > .26f && mouseRotation < 0) //if rotated more than ~30 degrees above the horizontal axis
        {
            Debug.Log("maxheight");
            currentRotation = prevRotation;
            //currentPos = prevPos;
            //currentPos += (player.transform.position - prevPlayerPos);
            //currentPos.y = prevPos.y;
        }
        else if (currentRotation.x < -.26f && mouseRotation >= 0) //if rotated more than ~30 degrees below the horizontal axis
        {
            currentRotation = prevRotation;
            //currentPos = prevPos;
            //currentPos += (player.transform.position - prevPlayerPos);
            //currentPos.y = prevPos.y;
        }
        else
        {
            //currentPos = transform.position;
            pos.y -= mouseRotation;

            //rotate camera
            transform.Rotate(mouseRotation, 0, 0);
            currentRotation = transform.rotation;
        }
        if(currentRotation.x < .26f && currentRotation.x > -.26f)
        {

            //currentPos = transform.position;
            pos.y -= mouseRotation;

            //rotate camera
            transform.Rotate(mouseRotation, 0, 0);
            currentRotation = transform.rotation;

        }

        if(pos.y < player.GetComponent<Player>().floor.SampleHeight(pos))
        {
            pos.y = player.GetComponent<Player>().floor.SampleHeight(pos);
        }

        transform.position = pos;

        transform.rotation = currentRotation;
        prevRotation = currentRotation;

        prevPlayerPos = player.transform.position;
        prevPos = currentPos;

    }
}
