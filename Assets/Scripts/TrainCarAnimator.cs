using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainCarAnimator : MonoBehaviour
{
    // this animates a train car in real time

    [SerializeField] Transform body, frontLook; // our front and back trucks
    [SerializeField] float rotFactor;


    private void FixedUpdate()
    {
        ProcessBodyRotation();
    }
    
    // process the way our body should face
    void ProcessBodyRotation()
    {
        body.transform.LookAt(frontLook);
    }
}
