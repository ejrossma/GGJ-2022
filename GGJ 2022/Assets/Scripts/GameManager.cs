using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public float difficulty; //base difficulty is 1
    private Enemy_Spawner es;

    // Start is called before the first frame update
    void Start()
    {
        es = GameObject.Find("Enemies").GetComponent<Enemy_Spawner>();

        for (int i = 0; i < 20; i+=2)
        {
            es.generateEnemy(new Vector3(-10.0f + i, 0.0f, 0.0f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
