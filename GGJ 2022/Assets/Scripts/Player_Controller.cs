using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public float playerSpeed = 200f;
    public float knockBackSpeed = 300f;
    public float knockBackTime = 0.2f;
    private float knockBackTimeElapsed = 0;
    private float inputX;
    private float inputY;
    public Vector2 inputVector;
    private Rigidbody2D playerRB;
    private bool knockedBack = false;
    private SpriteRenderer playerSR;
    
    private void Start() {
        playerRB = GetComponent<Rigidbody2D>();
        playerSR = GetComponent<SpriteRenderer>();
    }
    private void Update() {
        if(!knockedBack)
            PlayerInput();
        else
            knockBackTimeElapsed -= Time.deltaTime;
        if(knockBackTimeElapsed <= 0)
            knockedBack = false;
    }
    private void FixedUpdate() {
        Movement();
    }
    private void PlayerInput(){
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
        inputVector = new Vector2(inputX, inputY).normalized;
        if(inputX < 0){
            playerSR.flipX = false;
        }else if(inputX > 0){
            playerSR.flipX = true;
        }
    }
    private void Movement(){
        float speed = playerSpeed;
        if(knockedBack)
            speed = knockBackSpeed;
        playerRB.velocity = inputVector * Time.deltaTime * speed;
    }
    public void knockBack(Transform enemyTransform){
        knockBackTimeElapsed = knockBackTime;
        knockedBack = true;
        Vector3 direction = -(enemyTransform.position - this.transform.position).normalized;
        inputVector = direction;
    }
}
