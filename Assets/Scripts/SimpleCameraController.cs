using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameraController : MonoBehaviour
{
    /// this script controls the camera in the space. it is a simple camera controller
    /// 

    // our camera elements
    [SerializeField] Transform groundPoint; // the point that moves on the ground
    [SerializeField] Camera mainCamera; // our main camera

    [SerializeField] float zoomScale, moveScale, moveLerpSpeed, zoomLerp;

    float zoom; // our current zoom
    Vector4 move; // our x, y, z, and zoom 

    // runs at the start of the game
    private void Start()
    {
        // set our main camera
        mainCamera = Camera.main;

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
        zoom = -Input.mouseScrollDelta.y * zoomScale;

        // move the ground point around in the space using the WASD keys
        move = new Vector4(
            move.x + Input.GetAxis("Horizontal") * moveScale * Time.deltaTime, 
            0, 
            move.z + Input.GetAxis("Vertical") * moveScale * Time.deltaTime, zoom);

        // apply movement
        groundPoint.localPosition = Vector3.Lerp(groundPoint.localPosition, move, moveLerpSpeed * Time.deltaTime);
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, mainCamera.orthographicSize + zoom, zoomLerp * Time.deltaTime);
    }
}
