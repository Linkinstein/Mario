using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D bc2d;
    [SerializeField] private LayerMask platformLayerMask;

    [SerializeField] private bool mover = false;
    private bool stuck = true;

    public float x = 1;
    private float moveSpeed = 3f;

    private void Start()
    {
        StartCoroutine(stuckPhase());
    }

    private void FixedUpdate()
    {
        if (mover)
        {
            if (HittingWall() && !stuck) x = x * -1;
            rb.velocity = new Vector2(x * moveSpeed, rb.velocity.y);
        }
    }

    private bool HittingWall()
    {
        Vector2 dir = new Vector2(x, 0f);
        RaycastHit2D raycastHit = Physics2D.Raycast(bc2d.bounds.center, dir, bc2d.bounds.extents.x + 0.1f, platformLayerMask);
        return raycastHit.collider != null;
    }

    IEnumerator stuckPhase()
    {
        yield return new WaitForSeconds(0.25f);
        stuck = false;
    }
}