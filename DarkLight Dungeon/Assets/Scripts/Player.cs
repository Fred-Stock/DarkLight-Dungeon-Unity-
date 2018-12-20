using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	new void Update () {

        base.Update();

        CalcSteeringForces();
        Move();
        transform.LookAt(direction + transform.position);
	}

    /// <summary>
    /// moves player based on player inputs and external forces
    /// </summary>
    public override void CalcSteeringForces()
    {
        ApplyForce(PlayerInputForce());
    }

    protected override void Move()
    {
        acceleration = acceleration.normalized * maxSpeed;

        velocity += acceleration * Time.deltaTime;

        transform.position += acceleration * Time.deltaTime;

        Debug.Log(acceleration);

        direction = velocity.normalized;
        acceleration = Vector3.zero;

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
}
