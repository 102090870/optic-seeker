using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;
	
	public GameObject fadeEffect;

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
        TimeforCameraswitch -= Time.deltaTime;

        if (canSeePlayer)
        {
            fadeEffect.SetActive(true);
			dosomething.ChasePlayer();
        }
        else
        {
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
}
