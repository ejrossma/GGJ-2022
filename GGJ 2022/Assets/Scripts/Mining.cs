using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mining : MonoBehaviour
{
    public float MiningRate = 1f;
    private bool canMine = false;
    private float nextMetalMined = 0;
    private bool isMining = false;
    private void Update() {
        if(Input.GetKey(KeyCode.Mouse0))
            isMining = true;
        else
            isMining = false;

        if(canMine && isMining){
            Mine();
        }
    }
    private void Mine(){
        nextMetalMined += Time.deltaTime;
        if(nextMetalMined >= MiningRate){
            nextMetalMined = 0;
            FindObjectOfType<GameManager>().metal += 1;
        }
    }
    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.CompareTag("Mineable")){
            canMine = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
         if(other.gameObject.CompareTag("Mineable")){
            canMine = false;
        }
    }
}
