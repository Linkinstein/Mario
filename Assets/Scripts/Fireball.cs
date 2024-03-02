using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Fireball : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private CapsuleCollider2D cc2d;
    [SerializeField] private LayerMask platformLayerMask;

    public float x = 1;
    private float moveSpeed = 2.5f;

    private void Start()
    {
        rb.velocity = new Vector2(x * moveSpeed, rb.velocity.y);
    }

    private void FixedUpdate()
    {
        if (HittingWall()) Destroy(this.gameObject);
        if (isGrounded()) rb.velocity = new Vector2(x * moveSpeed, 3.5f);
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(cc2d.bounds.center, cc2d.bounds.size, 0f, Vector2.down, 0.1f, platformLayerMask);
        return raycastHit.collider != null;
    }

    private bool HittingWall()
    {
        Vector2 dir = new Vector2(x, 0f);
        RaycastHit2D raycastHit = Physics2D.Raycast(cc2d.bounds.center, dir, cc2d.bounds.extents.x + 0.1f, platformLayerMask);
        return raycastHit.collider != null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Monster>().Death(x, ' '); 
            Destroy(this.gameObject);
        }
    }
}