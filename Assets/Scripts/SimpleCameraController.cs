using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameraController : MonoBehaviour
{
    /// this script controls the camera in the space. it is a simple camera controller
    /// 

    // our camera elements
    [SerializeField] Transform groundPoint, rotationAlignmentContainer; // the point that moves on the ground
    [SerializeField] Camera mainCamera; // our main camera

    [SerializeField] float zoomScale, moveScale, moveLerpSpeed, zoomLerp, minZoom, maxZoom, rotLerp, newRot;

    [SerializeField] float zoom; // our current zoom
    Vector4 move; // our x, y, z, and zoom 

    // rotation stuff
    [SerializeField] float currentYRot; // our current Y rotation

    // runs at the start of the game
    private void Start()
    {
        // set our main camera
        mainCamera = Camera.main;

        // set our current y rot
        currentYRot = rotationAlignmentContainer.transform.localEulerAngles.y;

        // set our zoom
        zoom = mainCamera.orthographicSize;
    }

    // our fixed update runs 60 times per second
    private void Update()
    {
        ProcessInputs();
    }

    // process our inputs into camera movements in game
    void ProcessInputs()
    {
        // add to our zoom
        zoom = zoom + (-Input.mouseScrollDelta.y * zoomScale);

        // run our min/max zoom checks
        if (zoom < minZoom)
            zoom = minZoom;
        if (zoom > maxZoom)
            zoom = maxZoom;

        // move the ground point around in the space using the WASD keys
        move = new Vector4(
            move.x + Input.GetAxis("Horizontal") * moveScale * Time.deltaTime * zoom/2, 
            0, 
            move.z + Input.GetAxis("Vertical") * moveScale * Time.deltaTime * zoom/2, zoom);

        // also move with the mouse
        if (Input.GetMouseButton(2) || Input.GetMouseButton(1))
        {
            move = new Vector4(
                move.x + -Input.GetAxis("Mouse X") * moveScale * Time.deltaTime * zoom * 0.8f, // we multiply by 2 because it is hard to move with the mouse
                0,
                move.z + -Input.GetAxis("Mouse Y") * moveScale * Time.deltaTime * zoom * 1.5f, zoom);
        }

        // apply movement
        groundPoint.localPosition = Vector3.Lerp(groundPoint.localPosition, move, moveLerpSpeed * Time.deltaTime);
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, zoom, zoomLerp * Time.deltaTime);

        // handle our rotation
        if (Input.GetKeyDown(KeyCode.Q))
            currentYRot = currentYRot - 90;
        if (Input.GetKeyDown(KeyCode.E))
            currentYRot = currentYRot + 90;

        // apply rotation
        newRot = Mathf.Lerp(newRot, currentYRot, rotLerp * Time.deltaTime);
        rotationAlignmentContainer.transform.localEulerAngles = new Vector3(0, newRot, 0);




    }
}
