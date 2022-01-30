using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    //larger size = more hp & less speed
    //lesser size = less hp & more speed
    public int hp;
    public float speed;
    public int damage;
    
    public AIPath pathfind;
    private GameManager gm;
    private bool dying = false;

    private GameObject player;
    public Sprite explosion;
    public GameObject guts;
    public SpriteRenderer enemySprite;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        pathfind.maxSpeed = speed;       
    }

    // Update is called once per frame
    void Update()
    {
        if(hp <= 0 && !dying){
            StartCoroutine(Die());
            pathfind.maxSpeed = 0;
        }
    }
    private void takeDamage(int Damage){
        Debug.Log("Working");
        hp -= Damage;
    }
    private IEnumerator Die(){
        dying = true;
        GetComponent<Collider2D>().enabled = false;
        if(Random.Range(0.0f, 1.0f)  >= 0.6f)
            Instantiate(guts, this.transform.position, Quaternion.identity);
        enemySprite.sprite = explosion;
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
        yield return null;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "Player_Collider")
        {
            gm.takeDamage(damage);
            Debug.Log("The Player took " + damage + " damage!");
        }
        if(col.gameObject.CompareTag("BULLET")){
            takeDamage(col.gameObject.GetComponent<BulletScript>().Damage);
            Destroy(col.gameObject);
        }
    }
}
