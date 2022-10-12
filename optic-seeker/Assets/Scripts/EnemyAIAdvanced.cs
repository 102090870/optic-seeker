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

    public float turnorder = 0f;


    public LayerMask whatIsGround;


    //Patroling
    public Vector3 walkPoint1;
    public Vector3 walkPoint2;
    public Vector3 walkPoint3;
    public bool walkPointSet;

    private void Start()
    {
        walkPointSet = true;
    }
    private void Awake()
    {
        player = GameObject.Find("Character").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if(turnorder >= 3f)
        {
            turnorder = 0;
        }
    }

    public void Patroling2()
    {
        walkPoint1 = new Vector3(travelP1.position.x, transform.position.y, travelP1.position.z);
        walkPoint2 = new Vector3(travelP2.position.x, transform.position.y, travelP2.position.z);
        walkPoint3 = new Vector3(travelP3.position.x, transform.position.y, travelP3.position.z);


        if (turnorder < 1f && turnorder >= 0f)
        {
            agent.SetDestination(walkPoint1);

            Vector3 distanceToWalkPoint = transform.position - walkPoint1;

            if (distanceToWalkPoint.magnitude < 1f)
            {
                turnorder++;
            }
        }

        if (turnorder < 2f && turnorder >= 1f)
        {
            agent.SetDestination(walkPoint2);

            Vector3 distanceToWalkPoint = transform.position - walkPoint2;

            if (distanceToWalkPoint.magnitude < 1f)
            {
                turnorder++;
            }
        }

        if (turnorder < 3f && turnorder >= 2f)
        {
            agent.SetDestination(walkPoint1);

            Vector3 distanceToWalkPoint = transform.position - walkPoint1;

            if (distanceToWalkPoint.magnitude < 1f)
            {
                turnorder++;
            }
        }
    }

    public void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
}
