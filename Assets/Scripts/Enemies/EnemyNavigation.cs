using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Controls enemy navigation using Unity's NavMeshAgent.
/// Continuously sets the agent's destination to the player's position.
/// </summary>
public class EnemyNavigation : MonoBehaviour
{
    public Transform player; // Reference to the player's transform; assign in Inspector
    private NavMeshAgent agent; // NavMeshAgent component used for pathfinding

    /// <summary>
    /// Initializes the NavMeshAgent component.
    /// </summary>
    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent attached to this GameObject
    }

    /// <summary>
    /// Updates the agent's destination every frame if the player reference is set.
    /// </summary>
    void Update()
    {
        if (player != null) // Ensure the player reference is valid
        {
            agent.SetDestination(player.position); // Move towards the player's current position
        }
    }
}
