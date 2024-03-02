using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform mario;

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
    }
}
