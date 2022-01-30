using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public float difficulty; //base difficulty is 1
    public Enemy_Spawner es;

    public int metal = 0;
    public int guts = 0;
    public float playerHealth = 100f;
    public int points;

    public bool playerIframes;
    public Image HealthbarR, HealthbarL;
    public GameObject StartGame, GameOver;
    public Text MetalNum, GutsNum;
    private bool gameRunning = false;
    private bool deleteEnemies;

    void Start()
    {
        //for (int i = 0; i < 20; i+=2)
        //{
            //es.generateEnemy(new Vector3(-10.0f + i, 0.0f, 0.0f));
        //}
        FindObjectOfType<Player_Controller>().enabled = false;
        StartGame.SetActive(true);
        GameOver.SetActive(false);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !gameRunning){
            gameRunning = true;
            FindObjectOfType<Player_Controller>().enabled = true;
            StartGame.SetActive(false);
            GameOver.SetActive(false);
            destroyEnemiesAndTurrets();
        }
        if(gameRunning)
            runGame();
        else if(!StartGame.activeInHierarchy){
            GameOver.SetActive(true);
            FindObjectOfType<Player_Controller>().gameObject.transform.position = new Vector2(0f,0f);
            FindObjectOfType<Player_Controller>().enabled = false;
            haltEnemiesAndTurrets();
        }
    }

    private void runGame(){
        HealthbarR.fillAmount = HealthbarL.fillAmount = playerHealth / 100f;
        MetalNum.text = metal.ToString();
        GutsNum.text = guts.ToString();
        //Spawn Enemies
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
