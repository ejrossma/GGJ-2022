using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public float difficulty; //base difficulty is 1
    private Enemy_Spawner es;

    public int metal = 0;
    public int guts = 0;
    public float playerHealth = 100;

    public Text healthText;
    public bool playerIframes;

    void Start()
    {
        es = FindObjectOfType<Enemy_Spawner>();

        for (int i = 0; i < 20; i+=2)
        {
            es.generateEnemy(new Vector3(-10.0f + i, 0.0f, 0.0f));
        }
        healthText.text = "Health: " + playerHealth;
    }
    void Update()
    {
    }

    public void takeDamage(int damage, Transform enemyTransform)
    {
        FindObjectOfType<Player_Controller>().knockBack(enemyTransform);
        playerHealth -= damage;
        healthText.text = "Health: " + playerHealth;
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
