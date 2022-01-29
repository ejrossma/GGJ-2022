using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{
    public GameObject enemy;
    
    private Transform enemyContainer;


    // Start is called before the first frame update
    void Start()
    {
        enemyContainer = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void generateEnemies() {
        //choose size
        float enemySize = Random.Range(0.75f, 1.25f);
        //get health and speed based on size + time into game
        
    }
}
