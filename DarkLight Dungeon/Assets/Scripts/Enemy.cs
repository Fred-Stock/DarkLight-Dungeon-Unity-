﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {

    public GameObject player;

    [SerializeField]
    private Terrain floor;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        CalcSteeringForces();
        Move();
	}

    /// <summary>
    /// 
    /// </summary>
    public override void CalcSteeringForces()
    {
        ApplyForce(Seek(player.transform.position));
    }

    protected override void Move()
    {
        base.Move();
        Vector3 temp = transform.position;
        temp.y = floor.SampleHeight(transform.position) + 7.5f;
        transform.position = temp;
    }
}