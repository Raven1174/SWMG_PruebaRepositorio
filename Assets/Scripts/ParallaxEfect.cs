using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEfect : MonoBehaviour
{
    public float parallaxFX;
    private Transform camera;
    private Vector3 lastCamPosition;

    private void Start()
    {
        camera = Camera.main.transform;
        lastCamPosition = camera.position;
    }

    private void LateUpdate()
    {
        Vector3 bgMovement = camera.position - lastCamPosition;
        transform.position += new Vector3(bgMovement.x * parallaxFX, bgMovement.y, 0);
        lastCamPosition = camera.position;
    }
}
