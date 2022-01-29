using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public float playerSpeed = 5f;
    private float inputX;
    private float inputY;
    private Vector2 inputVector;
    private void Update() {
        PlayerInput();
        Movement();
    }
    private void PlayerInput(){
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
        inputVector = new Vector2(inputX, inputY).normalized * Time.deltaTime * playerSpeed;
    }
    private void Movement(){
        transform.position = new Vector2(transform.position.x + inputVector.x, transform.position.y + inputVector.y);
    }
}
