using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {

    private Vector3 mousePos;
    private Vector3 prevMousePos;

    //jumping fields
    private bool jumping;
    private Vector3 jumpVector;

    private Camera main;

    //temp maybe move to another script if jump gets moved
    [SerializeField]
    private Terrain floor;

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
                jumpVector = new Vector3(0, 100, 0);  
            }
            jumping = true;
        }

        mousePos = Input.mousePosition;

        if (Input.GetKeyDown(KeyCode.F))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        mouseRotation = Input.GetAxis("Mouse X");
        transform.Rotate(0, mouseRotation, 0);
       
        CalcSteeringForces();
        Move();

        prevMousePos = mousePos;
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
        //push them upwards with a vector
        //each frame reduce it by an amount
        //when at zero use a small downwards vector that increases as they go down that stops increaasing at certain value

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
                Debug.Log("jump" + jumpVector);
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
            Debug.Log("here");
            temp.y = floor.SampleHeight(temp) + 8.1f;

            transform.position = temp;
            jumpVector = Vector3.zero;
            jumping = false;

        }
        //else if(!jumping)
        //{
        //    ApplyGravity();
        //}

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
        vertAcceleration += new Vector3(0, -3, 0) / mass;
    }
}
