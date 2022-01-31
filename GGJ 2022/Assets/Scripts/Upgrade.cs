using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    private GameManager gm;
    private bool canUpgrade;
    private bool turretSelected;
    private bool upgrading;

    public GameObject notThisTurret;

    private GameObject selectedTurret;
    private List<GameObject> turretsInRange = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        notThisTurret = GameObject.Find("DummyT");
    }

    // Update is called once per frame
    void Update()
    {
        switch (turretsInRange.Count)
        {
            case 0:
                canUpgrade = false;
                upgrading = false;
                deselectTurret(selectedTurret);
                turretSelected = false;
                break;
            case 1:
                canUpgrade = true;
                if (turretsInRange[0] != selectedTurret)
                    selectTurret(turretsInRange[0]);
                break;
            default:
                canUpgrade = true;
                if (closestTurret() != selectedTurret)
                    selectTurret(closestTurret());
                break;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && turretSelected && canUpgrade && !upgrading)
        {
            upgrading = true;
            showUpgrades();
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0) && turretSelected && canUpgrade && upgrading && gm.metal >= 5 && selectedTurret.GetComponent<TowerScript>().upgraded != true) //metal upgrade
        {
            //do metal upgrade
            selectedTurret.GetComponent<TowerScript>().upgradeToMetal();
            showUpgrades();
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1) && turretSelected && canUpgrade && upgrading && gm.guts >= 5 && selectedTurret.GetComponent<TowerScript>().upgraded != true) //alien upgrade
        {
            //do alien upgrade
            selectedTurret.GetComponent<TowerScript>().upgradeToAlien();
            showUpgrades();
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0) && turretSelected && canUpgrade && upgrading && gm.metal >= 5 && selectedTurret.GetComponent<TowerScript>().upgraded == true)
        {
            selectedTurret.GetComponent<TowerScript>().upgradeMechTurret();
            showUpgrades();
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1) && turretSelected && canUpgrade && upgrading && gm.guts >= 5 && selectedTurret.GetComponent<TowerScript>().upgraded == true)
        {
            selectedTurret.GetComponent<TowerScript>().upgradeAlienTurret();
            showUpgrades();
        }
    }
    //show two upgrade paths
        //alien
            //icon will be resource
            //turns tower into slowing tower
        //second alien upgrade
            //gray out the metal one
            //does damage now
        //metal
            //icon will be resource
            //turns tower into double barrel
        //second metal upgrade
            //gray out the alien one
            //increased fire rate
    private void showUpgrades()
    {
        if (!selectedTurret.GetComponent<TowerScript>().fullyUpgraded) {
            if (selectedTurret.GetComponent<TowerScript>().getTowerType() == "alien")
            {
                //show upgrades + text
                selectedTurret.transform.GetChild(1).gameObject.SetActive(true);

                //hide mech upgrades
                selectedTurret.GetComponent<TowerScript>().metalCost.enabled = false;
                //hide metal icon
                selectedTurret.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);

                //hide mouse indicator
                selectedTurret.transform.GetChild(0).gameObject.SetActive(false);

                //show hammer
                transform.parent.GetChild(3).gameObject.SetActive(true);

            }
            else if (selectedTurret.GetComponent<TowerScript>().getTowerType() == "mech")
            {
                //show upgrades + text
                selectedTurret.transform.GetChild(1).gameObject.SetActive(true);

                //hide alien cost
                selectedTurret.GetComponent<TowerScript>().gutsCost.enabled = false;
                //hide guts icon
                selectedTurret.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);

                //hide mouse indicator
                selectedTurret.transform.GetChild(0).gameObject.SetActive(false);

                //show hammer
                transform.parent.GetChild(3).gameObject.SetActive(true);
            }
            else
            {
                //show upgrades
                selectedTurret.transform.GetChild(1).gameObject.SetActive(true);
                //hide mouse indicator
                selectedTurret.transform.GetChild(0).gameObject.SetActive(false);
                //show hammer
                transform.parent.GetChild(3).gameObject.SetActive(true);
            }
        } 
    }

    private void hideUpgrades()
    {
        selectedTurret.transform.GetChild(1).gameObject.SetActive(false);
        //hide hammer
        transform.parent.GetChild(3).gameObject.SetActive(false);
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
        if (turretSelected) {
            selectedTurret.transform.GetChild(0).gameObject.SetActive(false);
            hideUpgrades();
            selectedTurret = notThisTurret;
        }
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
            if (col.gameObject.GetComponent<TowerScript>().upgraded == false)
                turretsInRange.Add(col.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "TURRET")
        {
            if (col.gameObject.GetComponent<TowerScript>().upgraded == false)
                turretsInRange.Remove(col.gameObject);
        }
    }
}
