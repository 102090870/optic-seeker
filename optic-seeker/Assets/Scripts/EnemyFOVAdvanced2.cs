using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyFOVAdvanced2 : MonoBehaviour
{
    public float radius;
    [Range(0, 360)]
    public float angle;

    public Camera cam1;
    public Camera cam2;

    public FPSCAM Disrupt1;
    public FPSCAM2 Disrupt2;
    public EnemyAIAdvanced dosomething;
    public Outline activateOutline;
    public RawImage Crosshair;

    public GameObject playerRef;
    private PlayerMov playerMov;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;

    public int MoveSpeed = 4;
    public int MaxDist = 1;
    public int MinDist = 0;

    public float TimeforCameraswitch = 1.0f;

    private void Start()
    {
        Disrupt1.enabled = true;
        Disrupt2.enabled = false;
        activateOutline.enabled = false;
        cam1.enabled = true;
        cam2.enabled = false;
        playerRef = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        TimeforCameraswitch -= Time.deltaTime;

        if (canSeePlayer)
        {
            dosomething.ChasePlayer();
        }
        else
        {
            if (TimeforCameraswitch <= 1.0f && playerMov.isHidden == false)
            {
                timerEnded();

            }
            if (playerMov.isHidden == true)
            {
                isHiddenTimerEnded();

            }
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
                    canSeePlayer = true;
                else
                    canSeePlayer = false;
            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
            canSeePlayer = false;
    }
    private void timerEnded()
    {
        dosomething.Patroling2();
    }

    private void isHiddenTimerEnded()
    {
        dosomething.Patroling2();
    }
}
