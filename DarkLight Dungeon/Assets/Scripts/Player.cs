using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {

    private Vector3 mousePos;
    private Vector3 prevMousePos;

    //jumping fields
    private float jumpTimer;
    private float inAir;
    private bool jumping;

    private Camera main;

    //temp maybe move to another script if jump gets moved
    [SerializeField]
    private Terrain floor;

    float mouseRotation;

	// Use this for initialization
	void Start () {
        //main = Camera.main;
        inAir = 1f;
        jumpTimer = 0;
        jumping = false;
	}
	
	// Update is called once per frame
	new void Update () {

        base.Update();

        //temp section maybe move to another script
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumping = true;
        }

        mousePos = Input.mousePosition;

        //temporary section
        //if(mousePos.x >= main.orthographicSize)
        //{
        //    mousePos.x = -main.orthographicSize;
        //    Input.mousePosition = mousePos;
        //}

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
        Gravity();
        ApplyJumpForce(Jump());
        ApplyForce(PlayerInputForce());
    }

    protected override void Move()
    {
        acceleration = acceleration.normalized * maxSpeed;
        vertAcceleration = vertAcceleration.normalized * maxAirSpeed;

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
            inputVector -= right;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector += right;
        }
        //if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        //{
        //    velocity = Vector3.zero;
        //}

        return inputVector;
    }

    /// <summary>
    /// 
    /// </summary>
    private Vector3 Jump()
    {

        if (jumping)
        {
            jumpTimer += Time.deltaTime;
            if(jumpTimer >= inAir)
            {
                jumping = false;
                jumpTimer = 0;
            }
            
            return new Vector3(0, 5, 0);
        }

        return Vector3.zero;

    }

    /// <summary>
    /// 
    /// </summary>
    private void Gravity()
    {
        Vector3 temp = transform.position;


        if (transform.position.y <= floor.SampleHeight(transform.position) + 8)
        {
            temp.y = floor.SampleHeight(temp) + 8;

            transform.position = temp;

        }

        ApplyGravity();

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
