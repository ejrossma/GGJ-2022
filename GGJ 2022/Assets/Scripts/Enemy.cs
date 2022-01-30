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

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        pathfind.maxSpeed = speed;       
    }

    // Update is called once per frame
    void Update()
    {
        if(hp <= 0){
            //Die
            Destroy(this.gameObject);
        }
    }
    private void takeDamage(int Damage){
        Debug.Log("Working");
        hp -= Damage;
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
        }
    }
}
