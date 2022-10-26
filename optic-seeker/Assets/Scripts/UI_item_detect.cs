using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_item_detect : MonoBehaviour
{
    public float radius;
    [Range(0, 360)]
    public float angle;
    public GameObject playerRef;
    public bool canSeePlayer;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public Camera cam;
    public Transform objectTransform;
    Renderer m_Renderer;
    public RawImage detect1;

    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
        detect1.enabled = false;
        m_Renderer = GetComponent<Renderer>();
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

    // Update is called once per frame
    void Update()
    {
        if (canSeePlayer && m_Renderer.isVisible)
        {
            Vector3 pos = cam.WorldToScreenPoint(objectTransform.position);
            detect1.transform.position = pos;
            detect1.enabled = true;
        }
        else
        {
            detect1.enabled = false;
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
                {
                    canSeePlayer = true;
                }

            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
            canSeePlayer = false;
    }
}
