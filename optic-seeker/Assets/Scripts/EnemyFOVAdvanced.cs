using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFOVAdvanced : MonoBehaviour
{
    private PlayerMov playerMov;

    public float radius;
    [Range(0, 360)]
    public float angle;

    public Camera cam1;
    public Camera cam2;

    public FPSCAM Disrupt1;
    public FPSCAM2 Disrupt2;
    public EnemyAIAdvanced dosomething;

    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;

    public int MoveSpeed = 4;
    public int MaxDist = 1;
    public int MinDist = 0;

    private void Awake()
    {
        
    }

    private void Start()
    {
        Disrupt1.enabled = true;
        Disrupt2.enabled = false;
        cam1.enabled = true;
        cam2.enabled = false;
        playerRef = GameObject.FindGameObjectWithTag("Player");
        playerMov = playerRef.GetComponent<PlayerMov>();
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void Update()
    {
        if (canSeePlayer)
        {
            Disrupt1.enabled = false;
            Disrupt2.enabled = true;
            cam1.enabled = false;
            cam2.enabled = true;
            dosomething.ChasePlayer();
        }
        else
        {
            Disrupt1.enabled = true;
            Disrupt2.enabled = false;
            cam1.enabled = true;
            cam2.enabled = false;
            dosomething.Patroling2();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;


            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    if (!playerMov.isHidden)
                    {
                        canSeePlayer = true;
                    }                    
                else
                    canSeePlayer = false;
            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
            canSeePlayer = false;
    }
}
