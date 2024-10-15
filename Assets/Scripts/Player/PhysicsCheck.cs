using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    public float checkRaduis;   //��ⷶΧ
    public bool isGround;
    public LayerMask groundLayer;
    public Vector2 bottomOffset;

    void Update()
    {
        Check();        
    }

    private void Check()
    {
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRaduis, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRaduis);
    }
}
