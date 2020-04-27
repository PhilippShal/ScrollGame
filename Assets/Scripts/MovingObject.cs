using System;
using Assets.Scripts.Helpers;
using UnityEngine;

namespace Assets.Scripts
{
    public class MovingObject : MonoBehaviour
    {
        protected float bottomBound;
        protected float leftBound;
        protected float rightBound;
        protected float topBound;
        private Camera mainCamera;
        private Vector2 objectSize;
        private Vector2 screenBounds;
        private float screenHeight;

        protected bool IsPositionOutOfScreen()
        {
            if (gameObject.transform.position.x - objectSize.x > rightBound ||
                gameObject.transform.position.x + objectSize.x < leftBound ||
                gameObject.transform.position.y - objectSize.y > topBound ||
                gameObject.transform.position.y + objectSize.y < bottomBound)
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
            objectSize = gameObject.GetComponent<SpriteRenderer>().size;
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
