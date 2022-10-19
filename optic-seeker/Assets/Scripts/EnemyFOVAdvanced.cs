using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyFOVAdvanced : MonoBehaviour
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
    public PlayerMov playerMov;


    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;

    public int MoveSpeed = 4;
    public int MaxDist = 1;
    public int MinDist = 0;

    public float TimeforCameraswitch = 1.0f;

    //public GDTFadeEffect fadeEffect;
    public GameObject fadeCanvas;
    private bool fadeCheck = false;
    public GDTFadeEffect fadeScript;

    private void Start()
    {
        //fadeScript = fadeCanvas.GetComponent<GDTFadeEffect>();
        Disrupt1.enabled = true;
        Disrupt2.enabled = false;
        activateOutline.enabled = false;
        cam1.enabled = true;
        cam2.enabled = false;
        playerRef = GameObject.FindGameObjectWithTag("Player");
        //playerMov = playerRef.GetComponent<PlayerMov>();
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
            Crosshair.enabled = false;

            //blinking
            //fadeScript.lastColor = Color.white;
            fadeCanvas.SetActive(true);

            TimeforCameraswitch = 2f;
            Disrupt1.enabled = false;
            Disrupt2.enabled = true;
            activateOutline.enabled = true;
            cam1.enabled = false;
            cam2.enabled = true;
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

    private void timerEnded()
    {
       // fadeScript.lastColor = Color.white;
        Crosshair.enabled = true;
        Disrupt1.enabled = true;
        Disrupt2.enabled = false;
        activateOutline.enabled = false;
        cam1.enabled = true;
        cam2.enabled = false;
        dosomething.Patroling2();
      
        //blinking
        fadeCanvas.SetActive(false);
    }

    private void isHiddenTimerEnded()
    {
        Disrupt1.enabled = false;
        Disrupt2.enabled = false;
        activateOutline.enabled = false;
        cam1.enabled = true;
        cam2.enabled = false;
        dosomething.Patroling2();
    }

    IEnumerator fadeTimer()
    {
        yield return new WaitForSeconds(2);
    }
}
