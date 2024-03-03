using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Monster : MonoBehaviour
{
    GameObject gmGO;
    GameManager gm;
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private CapsuleCollider2D cc2d;
    [SerializeField] private LayerMask platformLayerMask;
    [SerializeField] private LayerMask playerLayerMask;
    [SerializeField] private LayerMask enemyLayerMask;

    private float x = -1;
    private float moveSpeed = 1f;

    [SerializeField] private bool turtle = false;
    [SerializeField] public bool shelled = false;
    [SerializeField] private bool alive = true;

    [SerializeField] private bool rezzing = false;
    [SerializeField] private bool stomped = false;
    [SerializeField] private bool murder = false;

    private void Start()
    {
        gmGO = GameObject.FindWithTag("GameManager");
        gm = gmGO.GetComponent<GameManager>();
    }

    private void Update()
    {
        handleAnims();
    }

    private void FixedUpdate()
    {
        if (alive && !shelled)
        {
            if (HittingWall()) x = x * -1;
            rb.velocity = new Vector2(x * moveSpeed, rb.velocity.y);
        }
        if (shelled & !alive)
        {
            if (HittingWall()) x = x * -1;
            rb.velocity = new Vector2(x * (moveSpeed*5), rb.velocity.y);
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
        if (collision.gameObject.CompareTag("Player"))
        {
            if (alive && !shelled) collision.gameObject.GetComponent<Mario>().Death();
            if (shelled && !alive) collision.gameObject.GetComponent<Mario>().Death();
            if (shelled && alive)
            {
                x = Mathf.Sign(this.gameObject.transform.position.x - collision.transform.position.x);
                alive = false;
                cc2d.excludeLayers = 0;
            }
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (shelled && !alive) collision.gameObject.GetComponent<Monster>().Death(x, ' ');
        }
    }

    public void Death(float dir, char cause)
    {
        switch (cause)
        {
            case 's':
                if (turtle)
                {
                    shelled = true;
                    rb.velocity = new Vector2(0, 0); 
                    StartCoroutine(resurrection());
                }
                else
                {
                    stomped = true;
                    cc2d.enabled = false;
                    rb.isKinematic = true;
                    alive = false;
                    StartCoroutine(destroySelf());
                }
                rb.velocity = new Vector2(0, 0);
                break;

            default:
                murder = true;
                cc2d.enabled = false;
                alive = false;
                rb.velocity = new Vector2(dir, 3f);
                StartCoroutine(destroySelf());
                break;
        }
    }
    IEnumerator resurrection()
    {
        yield return new WaitForSeconds(3);
        if (alive)
        { 
            rezzing = true;
        }
        yield return new WaitForSeconds(2);
        if (alive) shelled = false;
        rezzing = false;
    }
    private void handleAnims()
    {
        anim.SetFloat("x", x);
        anim.SetBool("rezzing", rezzing);
        anim.SetBool("stomped", stomped);
        anim.SetBool("murder", murder);
        anim.SetBool("shelled", shelled);
    }

    IEnumerator destroySelf()
    {
        gm.getScore(100);
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }
}