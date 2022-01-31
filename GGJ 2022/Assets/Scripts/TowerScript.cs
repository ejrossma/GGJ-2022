using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerScript : MonoBehaviour
{

    [SerializeField] private GameObject range;
    private CircleCollider2D rangeCollider;
    [SerializeField] private GameObject bullet;
    [SerializeField] private List<Sprite> spriteCollection = new List<Sprite>();
    [SerializeField] public int Damage = 100;
    [SerializeField] public GameObject Barrel;
    [SerializeField] Transform FiringPoint;
    [SerializeField] private float BarrelRotateSpeed = 20f;

    private GameManager gm;
    

    private List<GameObject> enemyList = new List<GameObject>();
    private Vector3 BarrelDirection;

    private string towerType = "base";
    private float fireRate = 0.25f;
    private float timeUntilFire = 0.25f;

    public bool upgraded;

    public Text metalCost;
    public Text gutsCost;


    //ALIEN TOWER
        //AOE & SLOWS
        //NO LONGER DOES DAMAGE

    //ALIEN UPGRADE
        //DOES DAMAGE

    //METAL TOWER
        //DOUBLE BARREL
    
    //METAL UPGRADE
        //INCREASED FIRE RATE


    public void updateTowerType(string type)
    {
        towerType = type;
        switch (towerType)
        {
            case "alien":
                //modify stats here
                break;
            case "mech":
                //modify stats here
                break;
        }
    }

    public string getTowerType()
    {
        return towerType;
    }



    // Start is called before the first frame update
    void Start()
    {
        CircleCollider2D rangeCollider = range.GetComponent<CircleCollider2D>();
        BarrelDirection = Barrel.transform.up;
        GameObject.Find("Game_Manager");
    }

    void Update()
    {
        Barrel.transform.up = Vector3.Lerp(Barrel.transform.up, BarrelDirection, BarrelRotateSpeed * Time.deltaTime);
        Barrel.transform.RotateAround(this.transform.position, Vector3.up, 20 * Time.deltaTime);
        timeUntilFire -= Time.deltaTime;
        if (timeUntilFire <= 0)
        {
            fire();
            timeUntilFire = fireRate;
        }
    }

    public void upgradeToAlien()
    {
        //remove normal tower visual
        transform.GetChild(3).gameObject.SetActive(false);
        //add new tower visual
        transform.GetChild(4).gameObject.SetActive(true);
        //change type of tower
        updateTowerType("alien");
        //spend resources
        gm.guts -= 5;
    }

    public void upgradeToMetal()
    {
        //remove normal tower visual
        transform.GetChild(3).gameObject.SetActive(false);
        //add new tower visual
        transform.GetChild(5).gameObject.SetActive(true);
        //change type of tower
        updateTowerType("mech");

        //spend resources
        gm.metal -= 5;
    }

    public void upgradeAlienTurret()
    {

    }

    public void upgradeMechTurret()
    {

    }

    private void fire()
    {
        //choose where to fire at (bullets will not be homing)

        if (enemyList.Count > 0)
        {
            //choose first character that is inside of the list in order to attack at
            GameObject currTarget = enemyList[0];
            GameObject currBullet = Instantiate(bullet, FiringPoint.position, Quaternion.identity);
            currBullet.GetComponent<BulletScript>().Damage = this.Damage;
            //currBullet.transform.LookAt(currTarget.GetComponent<Transform>());
            currBullet.GetComponent<BulletScript>().direction = BarrelDirection = (currTarget.transform.position - transform.position).normalized; 
        }
    }
    public void enemyEnteredArea(Collider2D enemy)
    {
        enemyList.Add(enemy.gameObject);
    }
    public void enemyExitedArea(Collider2D enemy)
    {
        enemyList.Remove(enemy.gameObject);
    }

    public void enemyStayedArea(Collider2D enemy)
    {
        bool alreadyExists = false;
        foreach(var enem in enemyList)
        {
            if (enem.gameObject == enemy.gameObject)
                alreadyExists = true;
        }
        if (!alreadyExists)
            enemyList.Add(enemy.gameObject);
    }
}
