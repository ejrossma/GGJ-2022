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
        
        //right mouse to open menu
        if (Input.GetKeyDown(KeyCode.Mouse1)) {
            if (turretSelected && canUpgrade && !upgrading)
            {
                upgrading = true;
                showUpgrades();
            }
            else if (turretSelected && upgrading && canUpgrade)
            {
                if (gm.guts >= 5 && !selectedTurret.GetComponent<TowerScript>().upgraded)
                {
                    //do guts upgrade
                    selectedTurret.GetComponent<TowerScript>().upgradeToAlien();
                    showUpgrades();
                }
                else if (gm.guts >= 5 && selectedTurret.GetComponent<TowerScript>().upgraded)
                {
                    //do 2nd guts upgrade
                    selectedTurret.GetComponent<TowerScript>().upgradeAlienTurret();
                    hideUpgrades();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            if (turretSelected && upgrading && canUpgrade)
            {
                if (gm.metal >= 5 && !selectedTurret.GetComponent<TowerScript>().upgraded)
                {
                    //do metal upgrade
                    selectedTurret.GetComponent<TowerScript>().upgradeToMetal();
                    showUpgrades();
                }
                else if (gm.metal >= 5 && selectedTurret.GetComponent<TowerScript>().upgraded)
                {
                    //do 2nd metal upgrade
                    selectedTurret.GetComponent<TowerScript>().upgradeMechTurret();
                    hideUpgrades();
                }
            }
        }
    }

    private void showUpgrades()
    {
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

    private void hideUpgrades()
    {
        selectedTurret.transform.GetChild(1).gameObject.SetActive(false);
        //hide hammer
        transform.parent.GetChild(3).gameObject.SetActive(false);
        //switch off upgrading
        upgrading = false;
    }

    //handle the selection of new turret and deselection of old one
    private void selectTurret(GameObject turretToBeSelected)
    {
        deselectTurret(selectedTurret);
        turretSelected = true;
        selectedTurret = turretToBeSelected;
        //turn on mouse above turret
        if (!turretToBeSelected.GetComponent<TowerScript>().fullyUpgraded)
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
