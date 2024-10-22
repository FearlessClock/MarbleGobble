﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameStateVariable gamestate = null;
    [SerializeField] private float startingAngle = 0;
    [Tooltip("The pourcentage of the screen that the user has to move his finger to do a full turn of the pipe")]
    [Range(0.1f, 1.5f)]
    [SerializeField] private float screenTravelRatio = 1;
    private float currentAngle = 0;
    private float calculatedAngle = 0;
    [SerializeField] private Vector2[] entrancePoints = null;
    [SerializeField] private Vector2[] connectionPoints = null;

    private Vector2 pressStartPosition;

    private bool didPressDown = false;

    private void Awake()
    {
        startingAngle = SimplifyAngle(angle: startingAngle);
        currentAngle = startingAngle;
        this.transform.rotation = Quaternion.Euler(0, 0, startingAngle);
    }
    void Update()
    {
        if(gamestate.value != GameStateVariable.GameState.Running)
        {
            return;
        }
        // When touch begin
        if (InputManager.InputExistsDown())
        {
            pressStartPosition = InputManager.GetInput(0);
            didPressDown = true;
        }
        else if (InputManager.InputExistsMoved())
        {
            if(!didPressDown)
            {
                return;
            }
            // Get the distance that the finger has moved
            Vector2 touchPoint = InputManager.GetInput(0);
            float distanceRatio = ((touchPoint.x - pressStartPosition.x) * screenTravelRatio) / Screen.width;

            calculatedAngle = 360 * distanceRatio + currentAngle;
            this.transform.rotation = Quaternion.Euler(0, 0, calculatedAngle);
        }
        else if (InputManager.InputExistsUp())
        {
            if (didPressDown)
            {
                didPressDown = false;
                currentAngle = calculatedAngle;
            }
        }
    }

    public static float SimplifyAngle(float angle)
    {
        if(angle > 360)
        {
            return angle - 360;
        }
        else if(angle < 0)
        {
            return angle + 360;
        }
        return angle;
    }
}
