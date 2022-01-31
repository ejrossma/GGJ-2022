using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    //larger size = more hp & less speed
    //lesser size = less hp & more speed
    public int hp;
    public float speed;
    public float damage;
    public int score;

    public bool frozen;
    
    public AIPath pathfind;
    private GameManager gm;
    private bool dying = false;

    private GameObject player;
    public Sprite explosion;
    public GameObject guts;
    public SpriteRenderer enemySprite;
    public Image HealthBarR;
    public Image HealthBarL;
    public Canvas Healthbar;
    private float totalHP;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gm = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        pathfind.maxSpeed = speed;
        Healthbar.worldCamera = Camera.main;
        totalHP = hp;
    }

    // Update is called once per frame
    void Update()
    {
        if(hp <= 0 && !dying){
            StartCoroutine(Die());
            pathfind.maxSpeed = 0;
        }
        HealthBarL.fillAmount = HealthBarR.fillAmount = hp/totalHP;
    }

    private void takeDamage(int Damage){
        hp -= Damage;
    }

    public void updateSpeed(float spd)
    {
        speed = spd;
        pathfind.maxSpeed = speed;
    }

    private IEnumerator Die(){
        dying = true;
        GetComponent<Collider2D>().enabled = false;
        gm.points += score;
        if(Random.Range(0.0f, 1.0f)  >= 0.6f)
            Instantiate(guts, this.transform.position, Quaternion.identity);
        enemySprite.sprite = explosion;
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
        yield return null;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("BULLET")){
            takeDamage(col.gameObject.GetComponent<BulletScript>().Damage);
            Destroy(col.gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D other) {
        if (other.name == "Player_Collider" && gm.playerIframes == false)
        {
            gm.takeDamage(damage, this.transform);
        }
    }
}
