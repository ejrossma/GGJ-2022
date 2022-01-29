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
    
    public AIPath pathfind;

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pathfind.maxSpeed = speed;       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
