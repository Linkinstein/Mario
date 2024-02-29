using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mario : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D bc2d;
    [SerializeField] private LayerMask platformLayerMask;
    [SerializeField] private LayerMask enemyLayerMask;


    [SerializeField] private float x = 0;

    [SerializeField] private float moveSpeed = 2.5f;

    [SerializeField] private float jumpForce = 5.7f;
    [SerializeField] private float jumpTime = 0.25f;
    [SerializeField] private float jumpTimeCounter = 0;

    private void Update()
    {
        checkFeet();

        if (isGrounded() && Input.GetButtonDown("Jump"))
        {
            jumpTimeCounter = jumpTime;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (Input.GetButton("Jump") && jumpTimeCounter > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonUp("Jump"))
        {
            jumpTimeCounter = 0;
        }
    }

    private void FixedUpdate()
    {
        x = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(x * moveSpeed, rb.velocity.y);

    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(bc2d.bounds.center, bc2d.bounds.size, 0f, Vector2.down, 0.1f, platformLayerMask);
        return raycastHit.collider != null;
    }

    private void checkFeet()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(bc2d.bounds.center, bc2d.bounds.size, 0f, Vector2.down, 0.1f, enemyLayerMask);
        Debug.DrawRay(bc2d.bounds.center - new Vector3(bc2d.bounds.extents.x, bc2d.bounds.extents.y), Vector2.down * 0.1f, Color.red);
        if (raycastHit.collider != null)
        {
            Monster monster = raycastHit.collider.GetComponent<Monster>();
            if (monster != null)
            {
                monster.Death(x);
            }
            rb.velocity = new Vector2(rb.velocity.x, 3f);
        }
    }
}
