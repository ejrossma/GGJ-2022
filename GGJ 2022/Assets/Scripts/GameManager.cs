using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public float difficulty = 1f; //base difficulty is 1
    public Enemy_Spawner es;
    public float enemySpawnRate = 3.5f;
    private int enemyCluster;
    private float enemySpawnWait;

    public int metal = 0;
    public int guts = 0;
    public float playerHealth = 100f;
    public int points;

    public bool playerIframes;
    public Image HealthbarR, HealthbarL;
    public GameObject StartGame, GameOver, Score;
    public GameObject[] SpawnPoints;
    public Text MetalNum, GutsNum;
    private bool gameRunning = false;
    private bool deleteEnemies;
    private float difficultyTimer = 0f;
    private float gracePeriod;

    void Start()
    {
        FindObjectOfType<Player_Controller>().enabled = false;
        StartGame.SetActive(true);
        GameOver.SetActive(false);
        Score.SetActive(false);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !gameRunning){
            gameRunning = true;
            FindObjectOfType<Player_Controller>().enabled = true;
            StartGame.SetActive(false);
            GameOver.SetActive(false);
            Score.SetActive(false);
            destroyEnemiesAndTurrets();
            gracePeriod = 5f;
            points = 0;
        }
        if(gameRunning){
            runGame();
            if(gracePeriod <=0)
                spawnEnemies();
            else
                gracePeriod -= Time.deltaTime;
        }else if(!StartGame.activeInHierarchy){
            difficulty = 1f;
            difficultyTimer = 0f;
            enemySpawnRate = 3.5f;
            GameOver.SetActive(true);
            Score.SetActive(true);
            Score.GetComponent<Text>().text = points.ToString();
            FindObjectOfType<Player_Controller>().gameObject.transform.position = new Vector2(0f,0f);
            FindObjectOfType<Player_Controller>().enabled = false;
            haltEnemiesAndTurrets();
        }

        
    }
    private void runGame(){
        HealthbarR.fillAmount = HealthbarL.fillAmount = playerHealth / 100f;
        MetalNum.text = metal.ToString();
        GutsNum.text = guts.ToString();
        //Difficulty Scaling
        difficulty = Mathf.Clamp(difficulty, 1, 3);
        enemySpawnRate = Mathf.Clamp(enemySpawnRate, 1.5f, 3.5f);
        if(difficultyTimer < 1f){
            difficultyTimer += Time.deltaTime;
        }else{
            difficulty += 0.0033f;
            enemySpawnRate -= 0.0033f;
            difficultyTimer = 0f;
        }
        if(playerHealth <= 0){
            gameRunning = false;
        }
    }
    private void spawnEnemies(){
        if(enemySpawnWait <= 0){
            enemySpawnWait = enemySpawnRate;
            Vector3 spawnLocation = SpawnPoints[Random.Range(0, SpawnPoints.Length)].transform.position;
            enemyCluster = Random.Range(1,5);
            if(difficulty >= 1 && difficulty < 2) enemyCluster = Mathf.Clamp(enemyCluster, 1, 2);
            if(difficulty >= 2 && difficulty < 3) enemyCluster = Mathf.Clamp(enemyCluster, 1, 3);
            if(difficulty >= 3 ) enemyCluster = Mathf.Clamp(enemyCluster, 2, 4);
            for(int i = 0; i < enemyCluster; i++){
                es.generateEnemy(spawnLocation);
            }
        }else{
            enemySpawnWait -= Time.deltaTime;
        }
    }
    private void haltEnemiesAndTurrets(){
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("ENEMY");
        foreach(GameObject enemy in allEnemies){
            enemy.GetComponent<Enemy>().enabled = false;
        }
        GameObject[] allTurrets = GameObject.FindGameObjectsWithTag("TURRET");
        foreach(GameObject turret in allTurrets){
            turret.GetComponent<TowerScript>().enabled = false;
        }
    }
    private void destroyEnemiesAndTurrets(){
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("ENEMY");
        foreach(GameObject enemy in allEnemies){
            Destroy(enemy);
        }
        GameObject[] allTurrets = GameObject.FindGameObjectsWithTag("TURRET");
        foreach(GameObject turret in allTurrets){
            Destroy(turret);
        }
    }

    public void takeDamage(float damage, Transform enemyTransform)
    {
        FindObjectOfType<Player_Controller>().knockBack(enemyTransform);
        playerHealth -= damage;
        playerIframes = true;
        StartCoroutine(takeIframesAway());

    }
    private IEnumerator takeIframesAway(){
        yield return new WaitForSeconds(1f);
        //change player sprite or animation here for hurt state
        //play sound of getting absolutly owned here
        playerIframes = false;
        yield return null;
    }
}
