using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerControl inputControl;
    public Vector2 inputDirection;                               //接受玩家输入的方向值

    private Rigidbody2D rb;
    private PhysicsCheck physicsCheck;

    private int lastFaceDir = 1;
    public bool isFliping;


    [Header("基本信息")]
    public bool isHurt = false;
    public float hurtFroce;
    public bool isDead;

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
        if(!isHurt)
        Move();
        
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveSpeed * inputDirection.x * Time.deltaTime, rb.velocity.y);

        int faceDir =(int) transform.localScale.x;

        if (inputDirection.x > 0)
        {
            faceDir = 1;
        }
        if (inputDirection.x < 0)
        {
            faceDir = -1;
        }

    //Flip Body
        transform.localScale = new Vector3(faceDir, 1, 1);

        IsFliping(faceDir);
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if(physicsCheck.isGround)
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);   //选择了Impluse施力模式
    }

    private void IsFliping(int currentFaceDir)
    {
        if (currentFaceDir != lastFaceDir)
        {
            isFliping = true;

            // 更新上一次的朝向
            lastFaceDir = currentFaceDir;
        }
        else
        {
            isFliping = false;
        }
        
    }

    public void GetHurt(Transform attacker)
    {
        isHurt = true;
        rb.velocity = Vector2.zero;

        Vector2 dir = new Vector2((transform.position.x - attacker.position.x), 0).normalized;

        rb.AddForce(dir * hurtFroce,ForceMode2D.Impulse);
    }

    public void PlayerDead()
    {
        isDead = true;
        inputControl.GamePlay.Disable();
    }

}
