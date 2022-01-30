using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    [SerializeField] float moveBy = 10f;
    public Vector3 direction = new Vector3(0,1,0);
    private float timer = 2.5f;
    public int Damage;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 move = direction * moveBy * Time.deltaTime;
        transform.position += move;
        timer -= Time.deltaTime;
        if(timer <= 0)
            Destroy(gameObject);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
    }
}
