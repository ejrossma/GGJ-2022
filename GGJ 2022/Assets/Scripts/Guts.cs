using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guts : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            FindObjectOfType<GameManager>().guts += 1;
            Destroy(this.gameObject);
        }
    }
}
