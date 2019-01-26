using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {

    //Movement fields
    public float mass;
    public float maxSpeed;
    public float maxAirSpeed;
    public Vector3 targetPosition;
    public Vector3 direction = new Vector3(1, 0, 0);  
    public Vector3 velocity = new Vector3(0, 0, 0);
    public Vector3 acceleration = new Vector3(0, 0, 0);
    public Vector3 vertAcceleration = new Vector3(0, 0, 0);
    public Vector3 right;
    public Vector3 forward;
    protected Vector3 futurePos;
    protected Vector3 pursueForce;
    protected Vector3 seekingForce;
    protected Vector3 desiredVelocity;


    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	protected void Update () {
        //if(direction == Vector3.zero)
        //{
        //    direction = new Vector3(1, 0, 0);
        //}

        forward = transform.forward;
        right = Quaternion.Euler(0, -90, 0) * direction;


    }

    /// <summary>
    /// applies a force to an object based on its mass
    /// </summary>
    /// <param name="force">force to apply</param>
    public void ApplyForce(Vector3 force)
    {
        acceleration += force / mass;
    }


    /// <summary>
    /// move object based on forces
    /// </summary>
    protected virtual void Move()
    {
        acceleration = acceleration.normalized * maxSpeed;
        velocity += acceleration * Time.deltaTime;

        transform.position += velocity * Time.deltaTime;

        direction = velocity.normalized;
        acceleration = Vector3.zero;
    }

    /// <summary>
    /// move object towards a target
    /// </summary>
    /// <param name="targetPosition"></param>
    /// <returns></returns>
    protected Vector3 Seek(Vector3 targetPosition)
    {
        desiredVelocity = new Vector3(targetPosition.x - transform.position.x, 0, targetPosition.z - transform.position.z);

        desiredVelocity = desiredVelocity.normalized * maxSpeed;

        seekingForce = desiredVelocity - velocity;

        return seekingForce;
    }

    /// <summary>
    /// makes object move away from target
    /// </summary>
    /// <param name="targetPosition"></param>
    /// <returns></returns>
    protected Vector3 Flee(Vector3 targetPosition)
    {
        desiredVelocity = -new Vector3(targetPosition.x - transform.position.x, 0, targetPosition.z - transform.position.z);

        desiredVelocity = desiredVelocity.normalized * maxSpeed;

        seekingForce = desiredVelocity - velocity;

        return seekingForce;
    }

    /// <summary>
    /// void method that calculates movement of the agent
    /// </summary>
    public abstract void CalcSteeringForces();



    //fields
    protected int health;
    protected int damage;
    protected bool invulnerable;
    protected bool hit;

    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    public int Damage
    {
        get { return damage; }
    }

    public bool Invulnerable
    {
        get { return invulnerable; }
        set { invulnerable = value; }
    }

    public bool Hit
    {
        get { return hit; }
        set { hit = value; }
    }

    //methods
    public virtual void TakeDamage(Player damaged, Character damager)
    {
        damaged.Health -= damager.Damage;
    }

    /// <summary>
    /// method for determining knockback
    /// </summary>
    /// <param name="attacker">character that damaged the character getting knocked backwards</param>
    public virtual void Knockback()
    {
        //write new knockback method or refactor old one 
    }
}

