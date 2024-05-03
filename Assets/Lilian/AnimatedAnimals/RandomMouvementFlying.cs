using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomNavMeshMovement : MonoBehaviour
{
    public Vector3 zoneCenter; // Center of the NavMesh area
    public float zoneRadius;   // Radius of the NavMesh area
    private Vector3 zoneCalcul;

    private NavMeshAgent agent;
    private Vector3 randomDestination;
    private bool BaBalloon = false;
    bool hasReachedDestination => Vector3.Distance(transform.position, randomDestination) < 0.1f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Invoke("DelayedStart", 10f); // Appeler la fonction DelayedStart après un délai de 10 secondes
    }

    // Fonction pour démarrer après le délai
    void DelayedStart()
    {
        BaBalloon = true;
        SetRandomDestination();
    }

    void Update()
    {
        if (BaBalloon == true)
        {    // If the agent has reached the random destination, set a new one
            if (!agent.pathPending && agent.remainingDistance < 0.1f)
            {
                SetRandomDestination();
            }
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

    // Update zoneCalcul
    void UpdateZoneCalcul()
    {
        zoneCalcul = new Vector3(zoneCenter.x, zoneCenter.y - 1f, zoneCenter.z);
    }

    // Optionally, you can visualize the NavMesh area in the editor for easier setup
    void OnDrawGizmosSelected()
    {
        UpdateZoneCalcul();
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(zoneCenter, zoneRadius);
    }
}