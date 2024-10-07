using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgent2D : MonoBehaviour
{
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Disable vertical movement and make the agent work in 2D
        agent.updateUpAxis = false; // This prevents the agent from changing its Z position.
        agent.updateRotation = false; // This prevents unwanted 3D rotation.
    }

    private void Update()
    {
        // Force the Z-axis position to remain at 0, keeping the agent in the XY plane
        Vector3 position = agent.transform.position;
        position.z = 0f; // Lock Z position to 0 for 2D navigation.
        agent.transform.position = position;
    }
}
