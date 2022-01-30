using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{

    [SerializeField] private GameObject range;
    private CircleCollider2D rangeCollider;
    [SerializeField] private GameObject bullet;
    [SerializeField] private List<Sprite> spriteCollection = new List<Sprite>();
    

    private List<GameObject> enemyList = new List<GameObject>();

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


    }

    void Update()
    {
        
        timeUntilFire -= Time.deltaTime;
        if (timeUntilFire <= 0)
        {
            fire();
            timeUntilFire = fireRate;
        }
    }



    private void fire()
    {
        print("Fire");
        //choose where to fire at (bullets will not be homing)

        if (enemyList.Count > 0)
        {
            //choose first character that is inside of the list in order to attack at
            GameObject currTarget = enemyList[0];
            GameObject currBullet = Instantiate(bullet, gameObject.transform.position, Quaternion.identity);
            //currBullet.transform.LookAt(currTarget.GetComponent<Transform>());
            currBullet.GetComponent<BulletScript>().direction = (currTarget.transform.position - transform.position).normalized; 
        }
    }
    public void enemyEnteredArea(Collider2D enemy)
    {
        enemyList.Add(enemy.gameObject);
        Debug.Log("Enemy entered range");
    }
    public void enemyExitedArea(Collider2D enemy)
    {
        enemyList.Remove(enemy.gameObject);
        Debug.Log("Enemy exited range");
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
