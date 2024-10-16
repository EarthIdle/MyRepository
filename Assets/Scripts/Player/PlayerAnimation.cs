using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private PhysicsCheck physicsCheck;
    private PlayerController playerController;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        playerController = GetComponent<PlayerController>();

    }

    private void Update()
    {
        SetAnimator();
    }

    public void SetAnimator()
    {
        anim.SetFloat("velocityX", MathF.Abs(rb.velocity.x));
        anim.SetFloat("velocityY", rb.velocity.y);
        anim.SetBool("isGround", physicsCheck.isGround);
        anim.SetBool("isDead", playerController.isDead);
    }

    public void PlayerHurt()
    {
        anim.SetTrigger("hurt");
    }
}
