using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform mario;
    [SerializeField] private Transform blocker;
    [SerializeField] private float distance;

    private void Update()
    {
        if (mario != null)
        {
            Vector3 newPosition = transform.position;
            if (mario.position.x > (transform.position.x - 1.75f)) 
            {
                newPosition.x = mario.position.x + 1.75f;
                transform.position = newPosition;
            }
        }

        if (blocker != null)
        {
            Vector3 newPosition = transform.position;
            newPosition.x = transform.position.x - distance;
            blocker.position = newPosition;
        }
    }
}