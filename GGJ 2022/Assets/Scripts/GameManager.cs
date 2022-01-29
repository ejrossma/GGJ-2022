using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public float difficulty; //base difficulty is 1
    private Enemy_Spawner es;

    public int metal = 0;
    public int guts = 0;
    public float playerHealth = 100;

    void Start()
    {
        es = FindObjectOfType<Enemy_Spawner>();

        for (int i = 0; i < 20; i+=2)
        {
            es.generateEnemy(new Vector3(-10.0f + i, 0.0f, 0.0f));
        }
    }
    void Update()
    {

    }
}