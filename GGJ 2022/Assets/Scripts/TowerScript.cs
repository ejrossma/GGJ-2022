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
    [SerializeField] public GameObject BarrelTwo;
    [SerializeField] Transform FiringPoint;
    [SerializeField] Transform FiringPointOne;
    [SerializeField] Transform FiringPointTwo;
    [SerializeField] private float BarrelRotateSpeed = 20f;

    private GameManager gm;
    

    private List<GameObject> enemyList = new List<GameObject>();
    private Vector3 BarrelDirection;

    private string towerType = "base";
    private float fireRate = 0.25f;
    private float timeUntilFire = 0.25f;

    private float slowRate = 0.75f;
    private float unfreezeRate = 1.33f;

    public bool upgraded;
    private bool fullyUpgraded;

    public Text metalCost;
    public Text gutsCost;

    private float mytimer = 0.5f;


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
        gm = GameObject.Find("Game_Manager").GetComponent<GameManager>();
    }

    void Update()
    {
        if(towerType == "base"){
            Barrel.transform.up = Vector3.Lerp(Barrel.transform.up, BarrelDirection, BarrelRotateSpeed * Time.deltaTime);
            //Barrel.transform.RotateAround(this.transform.position, Vector3.up, 20 * Time.deltaTime);
            timeUntilFire -= Time.deltaTime;
            if (timeUntilFire <= 0)
            {
                fire();
                timeUntilFire = fireRate;
            }
        }
        if(towerType == "mech"){
            BarrelTwo.transform.up = Vector3.Lerp(BarrelTwo.transform.up, -BarrelDirection, BarrelRotateSpeed * Time.deltaTime);
            //BarrelTwo.transform.RotateAround(this.transform.position, Vector3.up, 20 * Time.deltaTime);
            timeUntilFire -= Time.deltaTime;
            if (timeUntilFire <= 0)
            {
                Doublefire();
                timeUntilFire = fireRate;
            }
        }
        if(towerType == "alien"){
            mytimer -= Time.deltaTime;
            freeze();
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
        fullyUpgraded = true;
        //hide upgrade menu
        transform.GetChild(1).gameObject.SetActive(false);
    }

    public void upgradeMechTurret()
    {
        fullyUpgraded = true;
        transform.GetChild(1).gameObject.SetActive(false);
        //hide upgrade menu
         if (enemyList.Count > 0)
        {
            //choose first character that is inside of the list in order to attack at
            GameObject currTarget = enemyList[0];
            GameObject currBullet = Instantiate(bullet, FiringPoint.position, Quaternion.identity);
            currBullet.GetComponent<BulletScript>().Damage = this.Damage;
            currBullet.GetComponent<BulletScript>().direction = BarrelDirection = (currTarget.transform.position - transform.position).normalized; 
        }
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
            currBullet.GetComponent<BulletScript>().direction = BarrelDirection = (currTarget.transform.position - transform.position).normalized; 
        }
    }
    private void freeze()
    {
        if (fullyUpgraded)
        {
            if (mytimer <= 0)
            {
                foreach (var enem in enemyList) {
                    var eScript = enem.GetComponent<Enemy>();
                    eScript.takeDamage(4);
                }
                mytimer = 0.5f;
            }
        }
        else
        {
            foreach (var enem in enemyList)
            {
                var eScript = enem.GetComponent<Enemy>();
                if (!eScript.frozen)
                {
                    eScript.updateSpeed(eScript.speed * slowRate);
                    eScript.frozen = true;
                }
                    
            }
        }
    }

    private void Doublefire(){
        if (enemyList.Count > 0)
        {
            //choose first character that is inside of the list in order to attack at
            GameObject currTarget = enemyList[0];
            GameObject currBullet1 = Instantiate(bullet, FiringPointOne.position, Quaternion.identity);
            GameObject currBullet2 = Instantiate(bullet, FiringPointTwo.position, Quaternion.identity);
            currBullet1.GetComponent<BulletScript>().Damage = this.Damage;
            currBullet1.GetComponent<BulletScript>().direction = BarrelDirection = (currTarget.transform.position - transform.position).normalized;
            currBullet2.GetComponent<BulletScript>().Damage = this.Damage;
            currBullet2.GetComponent<BulletScript>().direction = BarrelDirection = (currTarget.transform.position - transform.position).normalized;
            
        }
    }
    public void enemyEnteredArea(Collider2D enemy)
    {
        enemyList.Add(enemy.gameObject);
    }
    public void enemyExitedArea(Collider2D enemy)
    {
        enemyList.Remove(enemy.gameObject);
        if (towerType == "alien")
        {
            enemy.gameObject.GetComponent<Enemy>().updateSpeed(enemy.gameObject.GetComponent<Enemy>().speed * unfreezeRate);
            enemy.gameObject.GetComponent<Enemy>().frozen = false;
        }
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
