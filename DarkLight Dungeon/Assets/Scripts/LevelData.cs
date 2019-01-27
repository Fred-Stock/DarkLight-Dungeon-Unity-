using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour {

    public List<GameObject> enemyList;
    public List<GameObject> rockList;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        CleanUp();
	}

    private void CleanUp()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            if (enemyList[i].GetComponent<Enemy>().hp <= 0)
            {
                GameObject.Destroy(enemyList[i]);
                enemyList.RemoveAt(i);
                i--;
            }
        }
    }
}
