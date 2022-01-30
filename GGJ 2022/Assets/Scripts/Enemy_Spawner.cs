using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{
    public GameObject enemy;

    public GameManager gm;

    public float baseHealth;
    public float baseSpeed;
    public float baseDamage;
    public float baseScore;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void generateEnemy(Vector3 position) {
        //choose size
        float enemySize = Random.Range(0.75f, 1.25f);
        //get health and speed based on size + time into game
        float enemyHealth = enemySize * baseHealth * gm.difficulty;
        float enemySpeed = (baseSpeed / enemySize) * gm.difficulty;
        float enemyDamage = enemySize * baseDamage * gm.difficulty;
        float enemyScore = enemySize * baseScore * gm.difficulty;

        GameObject spawnedEnemy = Instantiate(enemy, position, Quaternion.identity);
        spawnedEnemy.transform.parent = transform;
        spawnedEnemy.GetComponent<Enemy>().hp = (int) enemyHealth;
        spawnedEnemy.GetComponent<Enemy>().speed = enemySpeed;
        spawnedEnemy.GetComponent<Enemy>().damage = (int) enemyDamage;
        spawnedEnemy.GetComponent<Enemy>().score = (int) enemyScore;
        spawnedEnemy.transform.localScale = new Vector3(enemySize, enemySize, enemySize);
    }
}
