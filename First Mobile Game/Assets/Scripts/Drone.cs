using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    private float rotationSpeed = 100f;
    public GameObject pivotPoint;
    public Animator animator;

    void Start()
    {
        animator.SetBool("IsMoving", true);
    }

    void Update()
    { 
        transform.RotateAround(pivotPoint.transform.position, new Vector3(0, 0, 1), rotationSpeed * Time.deltaTime);
        
    }


}
