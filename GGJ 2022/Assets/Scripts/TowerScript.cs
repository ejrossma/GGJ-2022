using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    

    private List<GameObject> enemyList = new List<GameObject>();
    private Vector3 BarrelDirection;

    private string towerType = "base";
    private float fireRate = 0.25f;
    private float timeUntilFire = 0.25f;


    //ALIEN TOWER
        //AOE & SLOWS
        //NO LONGER DOES DAMAGE

    //ALIEN UPGRADE
        //DOES DAMAGE

    //METAL TOWER
        //DOUBLE BARREL
    
    //METAL UPGRADE
        //INCREASED FIRE RATE


    void updateTowerType(string type)
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



    // Start is called before the first frame update
    void Start()
    {
        CircleCollider2D rangeCollider = range.GetComponent<CircleCollider2D>();
        BarrelDirection = Barrel.transform.up;

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



    private void fire()
    {
        //choose where to fire at (bullets will not be homing)

        if (enemyList.Count > 0)
        {
            print("Fire");
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
