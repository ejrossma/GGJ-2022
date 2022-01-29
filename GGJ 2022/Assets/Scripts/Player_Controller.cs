using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public float playerSpeed = 5f;
    private float inputX;
    private float inputY;
    private Vector2 inputVector;
    private Rigidbody2D playerRB;
    
    private void Start() {
        playerRB = GetComponent<Rigidbody2D>();
    }
    private void Update() {
        PlayerInput();
    }
    private void FixedUpdate() {
        Movement();
    }
    private void PlayerInput(){
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
        inputVector = new Vector2(inputX, inputY).normalized;
    }
    private void Movement(){
        //transform.position = new Vector2(transform.position.x + inputVector.x, transform.position.y + inputVector.y);
        playerRB.velocity = inputVector * Time.deltaTime * playerSpeed;
    }
}
