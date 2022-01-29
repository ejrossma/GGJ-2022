using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{
    public GameObject enemy;
    private Transform enemyContainer;

    private GameManager gm;

    public int baseHealth;
    public int baseSpeed;

    // Start is called before the first frame update
    void Start()
    {
        enemyContainer = transform.GetChild(0);
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void generateEnemy(Vector3 position) {
        //choose size
        float enemySize = Random.Range(0.75f, 1.25f);
        //get health and speed based on size + time into game
        int enemyHealth = (int) (enemySize * baseHealth * gm.difficulty);
        int enemySpeed = (int) ((baseSpeed / (int) enemySize) * gm.difficulty);

        GameObject spawnedEnemy = Instantiate(enemy, position, Quaternion.identity);
        spawnedEnemy.transform.parent = enemyContainer;
        spawnedEnemy.GetComponent<Enemy>().hp = enemyHealth;
        spawnedEnemy.GetComponent<Enemy>().speed = enemySpeed;
        spawnedEnemy.transform.localScale = new Vector3(enemySize, enemySize, enemySize);
    }
}
