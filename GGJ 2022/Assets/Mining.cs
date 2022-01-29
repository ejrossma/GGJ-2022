using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mining : MonoBehaviour
{
    private bool canMine = false;
    private void Update() {
        if(canMine){
            Mine();
        }
    }
    private void Mine(){
        if(Input.GetKey(KeyCode.Mouse0)){
            //Switch to mining animation
            //Start collecting resources in a timed interval fashion
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
