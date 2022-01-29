using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public float difficulty; //base difficulty is 1
    public Enemy_Spawner es;

    public int metal = 0;
    public int guts = 0;
    public float playerHealth = 100;

    // Start is called before the first frame update
    void Start()
    {
        es = GameObject.Find("Enemies").GetComponent<Enemy_Spawner>();

        for (int i = 0; i < 10; i++)
        {
            es.generateEnemy(new Vector3(10.0f * i, 0.0f, 0.0f));
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
