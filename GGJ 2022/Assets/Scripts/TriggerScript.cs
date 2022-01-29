using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TowerScript parentScript;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "ENEMY")
        {
            parentScript.enemyEnteredArea(other);
        }
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "ENEMY")
        {
            parentScript.enemyExitedArea(other);
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
