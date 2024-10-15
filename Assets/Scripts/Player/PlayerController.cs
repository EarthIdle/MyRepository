using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerControl inputControl;
    public Vector2 inputDirection;                               //接受玩家输入的方向值

    private Rigidbody2D rb;
    private PhysicsCheck physicsCheck;

    [Header("Move Info")]
    public float moveSpeed;
    public float jumpForce;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();

        inputControl = new PlayerControl();

        inputControl.GamePlay.Jump.started += Jump;
    }

    

    private void OnEnable()
    {
        inputControl.Enable();
    }

    private void OnDisable()
    {
        inputControl.Disable();
    }

    private void Update()
    {
        inputDirection =  inputControl.GamePlay.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Move();
        
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveSpeed * inputDirection.x * Time.deltaTime, rb.velocity.y);
        int faceDir = (int)transform.localScale.x;

        if (inputDirection.x > 0)
            faceDir = 1;
        if (inputDirection.x < 0)
            faceDir = -1;

        //Flip Body
        transform.localScale = new Vector3(faceDir, 1, 1);
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if(physicsCheck.isGround)
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);   //选择了Impluse施力模式
    }

}
