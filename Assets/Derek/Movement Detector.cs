using UnityEngine;

public class MovementDetector : MonoBehaviour
{
    private Vector3 lastPosition;
    public bool isMoving = false;

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        // Check if the object has moved
        if (transform.position != lastPosition)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        lastPosition = transform.position;
    }
}
