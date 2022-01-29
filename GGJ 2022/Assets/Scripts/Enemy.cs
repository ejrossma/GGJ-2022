using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //larger size = more hp & less speed
    //lesser size = less hp & more speed
    public int hp;
    public int speed;

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
