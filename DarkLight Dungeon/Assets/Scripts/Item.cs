using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public GameObject player;
    public string Name;
    public GameObject item;
    public float radius;

	// Use this for initialization
	void Start () {
        item = gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        PlayerPickup();
	}

    private void PlayerPickup()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < radius + player.GetComponent<BoxCollider>().bounds.size.x/2)
        {
            player.GetComponent<PlayerData>().InventoryList.Add(this);
            Destroy(gameObject);
        }
    }
}
