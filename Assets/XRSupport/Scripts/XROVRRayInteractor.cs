using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Interactor used for interacting with interactables at a distance.  This is handled via raycasts
/// that update the current set of valid targets for this interactor.
/// </summary>
public class XROVRRayInteractor : MonoBehaviour
{
    public float raySize = 50.0f;
    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update(){
        Vector3 lineStart = transform.TransformDirection(Vector3.forward);
        Vector3 lineEnd = lineStart * raySize;
        lineRenderer.SetPosition(0, lineStart);
        lineRenderer.SetPosition(1, lineEnd);
        /*
        Debug.DrawRay(transform.position, fwd * 50, Color.green);
        RaycastHit objectHit;
        if (Physics.Raycast(transform.position, fwd, out objectHit, 50))
        {
            //do something if hit object ie
            targetObject = objectHit.collider.gameObject;
        }
        */
    }
}