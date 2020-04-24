using System;
using Assets.Scripts.Helpers;
using UnityEngine;

namespace Assets.Scripts
{
    public class MovingObject : MonoBehaviour
    {
        protected float bottomBound;
        private BackgroundScroller bgScroller;
        private float leftBound;
        private float rightBound;
        private Vector2 screenBounds;
        private float screenHeight;
        private float topBound;

        protected bool IsNextPositionOutOfScreen(float xShift, float yShift)
        {
            if (gameObject.transform.position.x + xShift > rightBound ||
                gameObject.transform.position.x + xShift < leftBound ||
                gameObject.transform.position.y + yShift > topBound ||
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
            bgScroller = GameObject.Find(Constants.MainCameraName).GetComponent<BackgroundScroller>();
            screenBounds = bgScroller.ScreenBounds;
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
            screenBounds = bgScroller.ScreenBounds;
            topBound = screenBounds.y;
            bottomBound = topBound - screenHeight;
        }
    }
}
