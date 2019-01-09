using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {

    //jumping fields
    private bool jumping;
    private Vector3 jumpVector;

    [SerializeField]
    private Camera playerCam;
    private Quaternion currentRotation;
    private Quaternion prevRotation;


    //temp maybe move to another script if jump gets moved
    public Terrain floor;

    float mouseRotation;

	// Use this for initialization
	void Start () {

        jumping = false;
        jumpVector = new Vector3(0, 0, 0);
	}
	
	// Update is called once per frame
	new void Update () {

        base.Update();

        //temp section maybe move to another script
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!jumping)
            {
                jumpVector = new Vector3(0, 115, 0);  
            }
            jumping = true;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            Cursor.lockState = CursorLockMode.None;
        }

       // transform.LookAt(playerCam.transform.forward + transform.position);

        //mouseRotation = Input.GetAxis("Mouse X");
        //transform.Rotate(0, mouseRotation, 0);
        //currentRotation = transform.rotation;


        //if(currentRotation == prevRotation)
        //{
        //    Debug.Log("current: " + currentRotation);
        //    Debug.Log("previous: " + prevRotation);
        //    Debug.Log("it bronk");
        //}
        //if (playerCam.transform.rotation.x == playerCam.GetComponent<PlayerCamera>().prevRotation.x)
        //{
        //    Debug.Log("here");
        //    currentRotation = prevRotation;
        //}

        CalcSteeringForces();
        Move();
        //currentRotation = transform.rotation;
        //transform.rotation = currentRotation;
        //prevRotation = transform.rotation;
	}

    /// <summary>
    /// moves player based on player inputs and external forces
    /// </summary>
    public override void CalcSteeringForces()
    {
        ApplyJumpForce(Jump());
        Gravity();
        ApplyForce(PlayerInputForce());
    }

    protected override void Move()
    {
        acceleration = acceleration.normalized * (float)maxSpeed;
        //vertAcceleration = vertAcceleration.normalized * (float)maxAirSpeed;
        vertAcceleration = Vector3.ClampMagnitude(vertAcceleration, maxAirSpeed);

        velocity += acceleration * Time.deltaTime;

        transform.position += acceleration * Time.deltaTime;
        transform.position += vertAcceleration * Time.deltaTime;

        Debug.Log(vertAcceleration);

        direction = velocity.normalized;
        acceleration = Vector3.zero;
        vertAcceleration = Vector3.zero;

    }

    /// <summary>
    /// method that moves player based on WASD inputs
    /// </summary>
    /// <returns></returns>
    private Vector3 PlayerInputForce()
    {
        Vector3 inputVector = Vector3.zero;
        right = Quaternion.Euler(0, 90, 0) * forward;

        if (Input.GetKey(KeyCode.W))
        {
            inputVector += forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector -= forward;
        }

        if (Input.GetKey(KeyCode.D))
        {
            inputVector += right;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector -= right;
        }


        return inputVector;
    }

    /// <summary>
    /// 
    /// </summary>
    private Vector3 Jump()
    {

        if (jumping)
        {
            
            if(jumpVector.y < 20f && jumpVector.y > 0)
            {
                jumpVector.y = -.1f;
            }
            else if(jumpVector.y < 0)
            {
                jumpVector = 1.5f * jumpVector;
                if(jumpVector.y > 5)
                {
                    jumpVector.y = 5;
                }
            }
            else{
                jumpVector = .90f * jumpVector;
            }
        }
        return jumpVector;


    }

    /// <summary>
    /// 
    /// </summary>
    private void Gravity()
    {
        Vector3 temp = transform.position;


        if (transform.position.y <= floor.SampleHeight(transform.position) + 8)
        {
            temp.y = floor.SampleHeight(temp) + 8.1f;

            transform.position = temp;
            jumpVector = Vector3.zero;
            jumping = false;

        }
        else if((transform.position.y > floor.SampleHeight(transform.position) + 8.1f) && !jumping)
        {
            ApplyGravity();
        }

    }

    /// <summary>
    /// apply forces related to jumping
    /// </summary>
    /// <param name="force"></param>
    private void ApplyJumpForce(Vector3 force)
    {
        vertAcceleration += force / mass;
    }

    /// <summary>
    /// Apply gravity force
    /// </summary>
    private void ApplyGravity()
    {
        vertAcceleration += new Vector3(0, -100, 0) / mass;
    }
}
