using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour {

    int iteration;//varaible to track which rock to break off

	// Use this for initialization
	void Start () {
        iteration = 0;
	}
	
	// Update is called once per frame
	void Update () {

	}

    //void OnMouseDown()
    //{
    //    CrumbleRock();
    //}

    /*
     * loop that iterates when player clicks on rock
     * Each time it iterates break of corresponding rock
     */

    public void CrumbleRock()
    {
        if(iteration < 12)
        {
            iteration++;
            Transform child = transform.Find("shard" + iteration);
            child.gameObject.AddComponent<Rigidbody>();
            child.GetComponent<Rigidbody>().drag = .05f;
            child.GetComponent<Rigidbody>().mass = 100;
    
        }
        
    }
}
