using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private CapsuleCollider2D cc2d;
    [SerializeField] private LayerMask platformLayerMask;
    [SerializeField] private LayerMask playerLayerMask;
    [SerializeField] private LayerMask enemyLayerMask;

    [SerializeField] private float x = -1;
    [SerializeField] private float moveSpeed = 1f;

    [SerializeField] private bool turtle = false;
    [SerializeField] private bool shelled = false;
    [SerializeField] private bool alive = true;

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        if (alive && !shelled)
        {
            if (HittingWall()) x = x * -1;
            rb.velocity = new Vector2(x * moveSpeed, rb.velocity.y);
        }
    }

    private bool HittingWall()
    {
        Vector2 dir = new Vector2(x, 0f);
        RaycastHit2D raycastHit = Physics2D.Raycast(cc2d.bounds.center, dir, cc2d.bounds.extents.x + 0.1f, platformLayerMask);
        return raycastHit.collider != null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision);
        if (collision.gameObject.CompareTag("Player"))
        {
            if (alive) collision.gameObject.GetComponent<Mario>().Death();
            if (shelled && !alive) collision.gameObject.GetComponent<Mario>().Death();
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (shelled && !alive) collision.gameObject.GetComponent<Monster>().Death(x, ' ');
        }
    }

    public void Death(float dir, char cause)
    {
        switch (cause)
        {//s
            case 'o':
                if (turtle)
                {

                }
                else
                {
                    cc2d.enabled = false;
                    rb.isKinematic = true;
                    alive = false;
                }
                rb.velocity = new Vector2(0, 0);
                break;

            default:
                cc2d.enabled = false;
                alive = false;
                rb.velocity = new Vector2(dir, 3f);
                break;
        }
    }
}
