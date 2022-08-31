using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIAdvanced : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround;

    //Patroling
    public Vector3 walkPoint2;
    bool walkPointSet2;
    public float walkPointRange2;

    private void Awake()
    {
        player = GameObject.Find("Character").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    public void Patroling2()
    {
        if (!walkPointSet2) SearchWalkPoint2();

        if (walkPointSet2)
            agent.SetDestination(walkPoint2);

        Vector3 distanceToWalkPoint = transform.position - walkPoint2;

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet2 = false;
    }

    private void SearchWalkPoint2()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange2, walkPointRange2);
        float randomX = Random.Range(-walkPointRange2, walkPointRange2);

        walkPoint2 = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomX);
        if (Physics.Raycast(walkPoint2, -transform.up, 2f, whatIsGround))
            walkPointSet2 = true;
    }

    public void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
}
