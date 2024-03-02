using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] Transform mario;

    private void Update()
    {
        if (mario != null)
        {
            // Keep the same X position as the target
            Vector3 newPosition = transform.position;
            newPosition.x = mario.position.x;
            transform.position = newPosition;
        }
    }
}
