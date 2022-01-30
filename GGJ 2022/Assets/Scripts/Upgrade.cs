using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    private GameManager gm;
    private bool canUpgrade;
    private bool turretSelected;

    private GameObject selectedTurret;

    private List<GameObject> turretsInRange = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("Game_Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (turretsInRange.Count)
        {
            case 0:
                canUpgrade = false;
                deselectTurret(selectedTurret);
                turretSelected = false;
                break;
            case 1:
                canUpgrade = true;
                selectTurret(turretsInRange[0]);
                break;
            default:
                canUpgrade = true;
                selectTurret(closestTurret());
                break;
        }
    }

    //handle the selection of new turret and deselection of old one
    private void selectTurret(GameObject turretToBeSelected)
    {
        deselectTurret(selectedTurret);
        turretSelected = true;
        selectedTurret = turretToBeSelected;
        selectedTurret.transform.GetChild(0).gameObject.SetActive(true);
    }

    private void deselectTurret(GameObject turretToBeDeselected)
    {
        if (turretSelected)
            selectedTurret.transform.GetChild(0).gameObject.SetActive(false);
    }

    private GameObject closestTurret()
    {
        GameObject closest = turretsInRange[0];
        foreach (GameObject turret in turretsInRange)
        {
            if (Vector2.Distance(turret.transform.position, transform.position) < Vector2.Distance(closest.transform.position, transform.position))
            {
                closest = turret;
            }
        }
        return closest;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "TURRET")
        {
            turretsInRange.Add(col.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "TURRET")
        {
            turretsInRange.Remove(col.gameObject);
        }
    }
}
