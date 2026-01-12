using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Reference to the player's Transform
    public Vector3 offset; // Desired offset from the player

    // Use LateUpdate for camera movement to ensure the player has moved first, preventing jitter
    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
        }
    }
}
