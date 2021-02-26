using System;
using UnityEngine;



public class RotationGunPlayer : BaseTank
{
    private Vector2 mousePos;
    private Camera cam;
    

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        base.RotationOnTarget(mousePos, SpeedRotation);

    }


}