﻿using System;
using Assets.Scripts.Helpers;
using UnityEngine;

namespace Assets.Scripts
{
    public class MovingObject : MonoBehaviour
    {
        protected float bottomBound;
        private float leftBound;
        private Camera mainCamera;
        private float rightBound;
        private Vector2 screenBounds;
        private float screenHeight;
        private float topBound;

        protected bool IsNextPositionOutOfScreenX(float xShift)
        {
            if (gameObject.transform.position.x + xShift > rightBound ||
                gameObject.transform.position.x + xShift < leftBound)
            {
                return true;
            }

            return false;
        }

        protected bool IsNextPositionOutOfScreenY(float yShift)
        {
            if (gameObject.transform.position.y + yShift > topBound ||
                gameObject.transform.position.y + yShift < bottomBound)
            {
                return true;
            }

            return false;
        }

        protected bool IsPositionOutOfScreen()
        {
            if (gameObject.transform.position.x > rightBound ||
                gameObject.transform.position.x < leftBound ||
                gameObject.transform.position.y > topBound ||
                gameObject.transform.position.y < bottomBound)
            {
                return true;
            }

            return false;
        }

        protected void Start()
        {
            mainCamera = GameObject.Find(Constants.MainCameraName).GetComponent<Camera>();
            screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
            topBound = screenBounds.y;
            bottomBound = -screenBounds.y;
            leftBound = -screenBounds.x;
            rightBound = screenBounds.x;
            screenHeight = topBound * 2;
        }

        protected void Update()
        {
            RefreshBounds();
        }

        private void RefreshBounds()
        {
            screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
            topBound = screenBounds.y;
            bottomBound = topBound - screenHeight;
        }
    }
}
