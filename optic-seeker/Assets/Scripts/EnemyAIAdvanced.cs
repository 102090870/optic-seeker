using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIAdvanced : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;
    public Transform travelP1;
    public Transform travelP2;
    public Transform travelP3;

    private float turnorder = 0f;


    public LayerMask whatIsGround;


    //Patroling
    public Vector3 walkPoint2;
    bool walkPointSet2;

    private void Awake()
    {
        player = GameObject.Find("Character").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    public void Patroling2()
    {
        if (turnorder < 1f)
        {
            if (!walkPointSet2) SearchWalkPoint1();

            if (walkPointSet2)
                agent.SetDestination(walkPoint2);

            Vector3 distanceToWalkPoint = transform.position - walkPoint2;

            if (distanceToWalkPoint.magnitude < 1f)
                walkPointSet2 = false;
        }

        if (turnorder < 1f)
        {
            if (!walkPointSet2) SearchWalkPoint2();

            if (walkPointSet2)
                agent.SetDestination(walkPoint2);

            Vector3 distanceToWalkPoint = transform.position - walkPoint2;

            if (distanceToWalkPoint.magnitude < 1f)
                walkPointSet2 = false;
        }

        if (turnorder < 1f)
        {
            if (!walkPointSet2) SearchWalkPoint3();

            if (walkPointSet2)
                agent.SetDestination(walkPoint2);

            Vector3 distanceToWalkPoint = transform.position - walkPoint2;

            if (distanceToWalkPoint.magnitude < 1f)
                walkPointSet2 = false;
        }
    }

    private void SearchWalkPoint1()
    {

        walkPoint2 = new Vector3(travelP1.position.x, transform.position.y, travelP1.position.z);
        if (Physics.Raycast(walkPoint2, -transform.up, 2f, whatIsGround))
            walkPointSet2 = true;
    }

    private void SearchWalkPoint2()
    {

        walkPoint2 = new Vector3(travelP2.position.x, transform.position.y, travelP2.position.z);
        if (Physics.Raycast(walkPoint2, -transform.up, 2f, whatIsGround))
            walkPointSet2 = true;
    }
    private void SearchWalkPoint3()
    {

        walkPoint2 = new Vector3(travelP3.position.x, transform.position.y, travelP3.position.z);
        if (Physics.Raycast(walkPoint2, -transform.up, 2f, whatIsGround))
            walkPointSet2 = true;
    }

    public void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
}
