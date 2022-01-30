using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public GameObject turret;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(turret, transform.position, Quaternion.identity);
        }
    }
}
