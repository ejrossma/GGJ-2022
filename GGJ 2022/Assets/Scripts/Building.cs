using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Building : MonoBehaviour
{
    public GameObject turret;
    private GameManager gm;
    private bool canBuild;

    private void Start()
    {
        gm = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        canBuild = true;
    }

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && canBuild && gm.metal >= 5)
        {
            buildTurret();
        }
    }

    private void buildTurret()
    {
        gm.metal -= 5;
        Instantiate(turret, transform.position, Quaternion.identity);
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "TURRET")
        {
            canBuild = false;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "TURRET")
        {
            canBuild = true;
        }
    }
    
    
}
