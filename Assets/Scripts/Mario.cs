using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mario : MonoBehaviour
{
    [SerializeField] private Sprite bigun;
    [SerializeField] private Sprite smallun;


    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D bc2d;
    [SerializeField] private LayerMask platformLayerMask;
    [SerializeField] private LayerMask enemyLayerMask;


    [SerializeField] private float x = 0;

    [SerializeField] private float moveSpeed = 2.5f;

    [SerializeField] private float jumpForce = 5.7f;
    [SerializeField] private float jumpTime = 0.25f;
    [SerializeField] private float jumpTimeCounter = 0;

    [SerializeField] private bool big = false;
    [SerializeField] private bool flower = false;
    [SerializeField] private bool starred = false;
    [SerializeField] private bool alive = true;

    private void Start()
    {

    }


    private void Update()
    {
        if (alive)
        {
            if (!isGrounded())
            {
                checkFeet();
                checkHead();
            }

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
    }
    private void FixedUpdate()
    {
        if (alive)
        {
            x = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(x * moveSpeed, rb.velocity.y);
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(bc2d.bounds.center, bc2d.bounds.size, 0f, Vector2.down, 0.1f, platformLayerMask);
        return raycastHit.collider != null;
    }

    private void checkHead()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(bc2d.bounds.center, bc2d.bounds.size, 0f, Vector2.up, 0.1f, platformLayerMask);
        if (raycastHit.collider != null)
        {
            Debug.Log(raycastHit.collider != null);
        }
    }

    private void checkFeet()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(bc2d.bounds.center, bc2d.bounds.size, 0f, Vector2.down, 0.1f, enemyLayerMask);
        if (raycastHit.collider != null)
        {
            Monster monster = raycastHit.collider.GetComponent<Monster>();
            if (monster != null)
            {
                monster.Death(x, 's');
            }
            rb.velocity = new Vector2(rb.velocity.x, 3f);
        }
    }

    public void Death()
    {
        if (!starred)
        {
            if (!big)
            {
                bc2d.enabled = false;
                alive = false;
                rb.velocity = new Vector2(0, 3f);
            }
            else
            {
                SpriteRenderer sr = this.gameObject.GetComponent<SpriteRenderer>();
                big = false;
                flower = false;
                sr.sprite = smallun;
                bc2d.size = new Vector2(bc2d.size.x, bc2d.size.y / 2);
            }
        }
    }

    public void Morb()
    {
        SpriteRenderer sr = this.gameObject.GetComponent<SpriteRenderer>();
        if (big && !flower)
        {
            sr.color = Color.red;
            flower = true;
        }
        else if (!big)
        {
            sr.sprite = bigun;
            bc2d.size = new Vector2(bc2d.size.x, bc2d.size.y * 2);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
