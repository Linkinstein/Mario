using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Mario mario = collision.GetComponent<Mario>();
            mario.starred = false;
            mario.big = false;
            mario.Death();
        }

        if (collision.CompareTag("Enemy") || collision.CompareTag("Shroom") || collision.CompareTag("Star"))
        {
            Destroy(collision.gameObject);
        }
    }
}
