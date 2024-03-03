using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] gameObjects;
    bool active = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && active)
        {
            foreach(GameObject objects in gameObjects)
            { 
                objects.SetActive(true);
            }
        }
    }
}
