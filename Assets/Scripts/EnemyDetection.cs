using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public Transform player; // Reference to the player object
    public float detectionRadius = 35f; // Detection radius of the enemy
    public float moveSpeed = 5f; // Speed at which the enemy follows the player
    private Vector3 originalPosition; // Original position of the enemy
    private bool isPlayerDetected = false; // Flag to check if the player is detected

    private void Start()
    {
        // Save the enemy's original position
        originalPosition = transform.position;
    }

    private void Update()
    {
        Vector3 targetPosition = isPlayerDetected ? player.position : originalPosition;
        MoveToPosition(targetPosition);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform == player)
        {
            Debug.Log("Player detected!");
            isPlayerDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform == player)
        {
            Debug.Log("Player escaped!");
            isPlayerDetected = false;
        }
    }

    private void MoveToPosition(Vector3 targetPosition)
    {
        // Calculate direction to the target position
        Vector3 direction = (targetPosition - transform.position).normalized;

        // Move towards the target position
        float step = moveSpeed * Time.deltaTime; // Calculate the distance to move
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
    }

    private void OnDrawGizmos()
    {
        // Visualize the detection radius in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
