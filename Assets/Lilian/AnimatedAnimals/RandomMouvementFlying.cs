using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomNavMeshMovement : MonoBehaviour
{
    public Vector3 zoneCenter; // Center of the NavMesh area
    public float zoneRadius;   // Radius of the NavMesh area

    private NavMeshAgent agent;
    private Vector3 randomDestination;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetRandomDestination();
    }

    void Update()
    {
        // If the agent has reached the random destination, set a new one
        if (!agent.pathPending && agent.remainingDistance < 0.1f)
        {
            SetRandomDestination();
        }
    }

    void SetRandomDestination()
    {
        // Generate a random point within the defined NavMesh area
        randomDestination = zoneCenter + Random.insideUnitSphere * zoneRadius;

        // Project the random point onto the NavMesh
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDestination, out hit, zoneRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }

    // Optionally, you can visualize the NavMesh area in the editor for easier setup
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(zoneCenter, zoneRadius);
    }
}

