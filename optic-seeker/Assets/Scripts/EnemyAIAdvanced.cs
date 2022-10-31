using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIAdvanced : MonoBehaviour
{
    public NavMeshAgent agent;
    public PlayerMov damage;

    public Transform player;
    public Transform travelP1;
    public Transform travelP2;
    public Transform travelP3;
    public Transform travelP4;

    public float turnorder = 0f;


    public LayerMask whatIsGround;

    public float Attackcooldown;
    public float Attacktimer;


    //Patroling
    public Vector3 walkPoint1;
    public Vector3 walkPoint2;
    public Vector3 walkPoint3;
    public Vector3 walkPoint4;
    public bool walkPointSet;
    public float pausetime = 0f;

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

        if (Attackcooldown > 0)
        {
            Attackcooldown -= Time.deltaTime;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Attackcooldown <= 0)
            {
                damage.TakeDamage(1);
                Attackcooldown = Attacktimer;

            }
        }
    }

    public void Patroling2()
    {
        pausetime = 0;
        agent.speed = 6;
        agent.angularSpeed = 120;
        walkPoint1 = new Vector3(travelP1.position.x, transform.position.y, travelP1.position.z);
        walkPoint2 = new Vector3(travelP2.position.x, transform.position.y, travelP2.position.z);
        walkPoint3 = new Vector3(travelP3.position.x, transform.position.y, travelP3.position.z);
        walkPoint4 = new Vector3(travelP4.position.x, transform.position.y, travelP4.position.z);


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
            agent.SetDestination(walkPoint3);

            Vector3 distanceToWalkPoint = transform.position - walkPoint3;

            if (distanceToWalkPoint.magnitude < 1f)
            {
                turnorder++;
            }
        }

        if (turnorder < 4f && turnorder >= 3f)
        {
            agent.SetDestination(walkPoint4);

            Vector3 distanceToWalkPoint = transform.position - walkPoint4;

            if (distanceToWalkPoint.magnitude < 1f)
            {
                turnorder++;
            }
        }
    }

    public void ChasePlayer()
    {

        pausetime += Time.deltaTime;

        if (pausetime < 1)
        {
            agent.speed = 0;
            agent.angularSpeed = 0;
        }
        else
        {
            agent.speed = 20;
            agent.angularSpeed = 120;
            agent.SetDestination(player.position);
        }
    }
}
