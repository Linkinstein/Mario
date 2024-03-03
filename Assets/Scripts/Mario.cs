using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.IK;

public class Mario : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject fireball;
    [SerializeField] private SpriteRenderer sr;
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

    private int fireCount = 0;

    private void Update()
    {
        handleAnims();
        if (alive)
        {
            if (starred)
            {
                if (sr.color == Color.yellow) sr.color = Color.white;
                else sr.color = Color.yellow;
            }
            else if (flower) sr.color = new Color(255f, 189f, 189f);
            else sr.color = Color.white;

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

            if (flower && (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.LeftShift)) && fireCount<2)
            {
                fireCount++;
                Vector3 spawnPOS = this.gameObject.transform.position;
                spawnPOS.x = spawnPOS.x * x;
                GameObject fireballinstance = Instantiate(fireball, spawnPOS, this.gameObject.transform.rotation);
                fireballinstance.GetComponent<Fireball>().x = x;
                if (fireCount >= 2) StartCoroutine(fireCooldown());
            }
        }
    }

    IEnumerator fireCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        fireCount = 0;
    }

    private void FixedUpdate()
    {
        if (alive)
        {
            if (Input.GetAxis("Horizontal")!=0) x = Mathf.Sign(Input.GetAxis("Horizontal"));
            rb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rb.velocity.y);
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
            Treasure treasure = raycastHit.collider.GetComponent<Treasure>();
            if (treasure != null) 
            {
                treasure.hit(x,big);
            }
        }
    }

    private void checkFeet()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(bc2d.bounds.center, bc2d.bounds.size, 0f, Vector2.down, 0.15f, enemyLayerMask);
        if (raycastHit.collider != null)
        {
            Monster monster = raycastHit.collider.GetComponent<Monster>();
            if (monster != null)
            {
                if (!monster.shelled) monster.Death(x, 's');
            }
            rb.velocity = new Vector2(rb.velocity.x, 6f);
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
                sr.color = Color.white;
                big = false;
                flower = false;
                bc2d.size = new Vector2(bc2d.size.x, bc2d.size.y / 2);
                if (x>0) anim.Play("small idle right", -1, 0);
                else anim.Play("small idle left", -1, 0);
            }
        }
    }

    public void Morb()
    {
        if (big && !flower)
        {
            sr.color = new Color(255f, 189f, 189f);
            flower = true;
        }
        else if (!big)
        {
            if (x > 0) anim.Play("big idle right", -1, 0);
            else anim.Play("big idle left", -1, 0);
            big = true;
            bc2d.size = new Vector2(bc2d.size.x, bc2d.size.y * 2);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision);
        if (collision.gameObject.CompareTag("Shroom"))
        {
            Morb();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Enemy") && starred)
        {
            collision.gameObject.GetComponent<Monster>().Death(x,' ');
        }

        if (collision.gameObject.CompareTag("Star"))
        {
            starred = true;
            Destroy(collision.gameObject);
            StartCoroutine(starPower());
        }
    }

    private void handleAnims()
    {
        anim.SetFloat("x", x);
        anim.SetInteger("dir2", (int)Input.GetAxis("Shmorizontal"));
        anim.SetBool("isGrounded",isGrounded());
        anim.SetBool("alive", alive);
    }

    IEnumerator starPower()
    {
        yield return new WaitForSeconds(10f);
        starred = false;
    }
}
