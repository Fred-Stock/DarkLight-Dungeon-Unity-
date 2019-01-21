using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        PlayerPickup();
	}

    protected override void PlayerPickup()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < radius + player.GetComponent<BoxCollider>().bounds.size.x / 2)
        {
            player.GetComponent<PlayerData>().currency++;
            Destroy(gameObject);
        }
    }
}
