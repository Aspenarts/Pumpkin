using UnityEngine;

public class VisionCone : MonoBehaviour
{
    public float visionAngle = 45f; // Field of view angle
    public float visionDistance = 10f; // Maximum detection distance
    private bool isFacingRight = true; // Track whether the enemy is facing right or left

    // Set the facing direction from outside (based on movement direction)
    public void SetFacingDirection(bool facingRight)
    {
        isFacingRight = facingRight;
    }

    // Check if the target is within the vision cone
    public bool IsTargetInVision(GameObject target)
    {
        Vector3 directionToTarget = target.transform.position - transform.position;

        // Check distance
        if (directionToTarget.magnitude > visionDistance)
        {
            return false; // Target is too far
        }

        // Calculate the dot product based on the direction the enemy is facing
        Vector3 facingDirection = isFacingRight ? transform.right : -transform.right;

        // Check if the target is within the vision cone based on facing direction
        float dotProduct = Vector3.Dot(facingDirection, directionToTarget.normalized);

        // Convert the field of view angle to a dot product value for comparison
        float threshold = Mathf.Cos(visionAngle * 0.5f * Mathf.Deg2Rad);

        if (dotProduct >= threshold)
        {
            Debug.Log($"{target.name} is inside the vision cone!");
            return true; // Target is within the vision cone
        }
        else
        {
            return false; // Target is outside the vision cone
        }
    }

    // Visualize the vision cone and detected objects in the Scene View
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionDistance);

        // Draw vision cone based on facing direction
        Vector3 facingDirection = isFacingRight ? transform.right : -transform.right;
        Vector3 rightBoundary = Quaternion.Euler(0, 0, visionAngle * 0.5f) * facingDirection * visionDistance;
        Vector3 leftBoundary = Quaternion.Euler(0, 0, -visionAngle * 0.5f) * facingDirection * visionDistance;

        Gizmos.DrawLine(transform.position, transform.position + rightBoundary);
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary);
    }
}
