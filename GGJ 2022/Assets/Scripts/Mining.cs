using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mining : MonoBehaviour
{
    public float MiningRate = 1f;
    public GameObject Pickaxe;
    private bool canMine = false;
    private float nextMetalMined = 0;
    private bool isMining = false;
    private void Start() {
        Pickaxe.SetActive(false);
    }
    private void Update() {
        if(Input.GetKey(KeyCode.Mouse0))
            isMining = true;
        else
            isMining = false;

        if(canMine && isMining){
            Mine();
        }else{
            Pickaxe.SetActive(false);
        }
    }
    private void Mine(){
        Pickaxe.SetActive(true);
        nextMetalMined += Time.deltaTime;
        if(nextMetalMined >= MiningRate){
            nextMetalMined = 0;
            FindObjectOfType<GameManager>().metal += 1;
            //update canvas
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
