using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : Player {

    private List<GameObject> enemyList;

    public float attackRange;

	// Use this for initialization
	void Start () {
        managerData = this.GetComponent<Player>().manager.GetComponent<LevelData>();
        //enemyList = managerData.enemyList;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            for(int i = 0; i < managerData.enemyList.Count; i++)
            {
                Vector3 vecToEnemy = managerData.enemyList[i].transform.position - transform.position;
                float dotForward = Vector3.Dot(vecToEnemy, transform.forward);
    
                if (vecToEnemy.sqrMagnitude <= Mathf.Pow(attackRange, 2))
                { 
                    if(dotForward > 0)
                    {
                        managerData.enemyList[i].GetComponent<Enemy>().hp--;
                    }
                }
    
            }
        }

	}
}
