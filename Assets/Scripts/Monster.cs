using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private CapsuleCollider2D cc2d;
    [SerializeField] private LayerMask platformLayerMask;

    [SerializeField] private float x = -1;
    [SerializeField] private float moveSpeed = 1f;

    private void FixedUpdate()
    {
        if (hittingWall()) x = x * -1;

        rb.velocity = new Vector2(x * moveSpeed, rb.velocity.y);
    }

    private bool hittingWall()
    {
        Vector2 dir = new Vector2(x, 0f);
        RaycastHit2D raycastHit = Physics2D.Raycast(cc2d.bounds.center, dir, cc2d.bounds.extents.x + 0.1f, platformLayerMask);
        return raycastHit.collider != null;
    }
}
