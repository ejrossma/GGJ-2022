using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{
    public GameObject enemy;
    private Transform enemyContainer;

    private GameManager gm;

    public int baseHealth = 10;
    public int baseSpeed = 5;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    public void generateEnemy(Vector3 position) {
        //choose size
        float enemySize = Random.Range(0.75f, 1.25f);
        //get health and speed based on size + time into game
        int enemyHealth = (int) (enemySize * baseHealth * gm.difficulty);
        int enemySpeed = (int) ((baseSpeed / enemySize) * gm.difficulty);

        GameObject spawnedEnemy = Instantiate(enemy, position, Quaternion.identity);
        spawnedEnemy.transform.parent = transform;
        spawnedEnemy.GetComponent<Enemy>().hp = enemyHealth;
        spawnedEnemy.GetComponent<Enemy>().speed = enemySpeed;
        spawnedEnemy.transform.localScale = new Vector3(enemySize, enemySize, enemySize);
    }
}
