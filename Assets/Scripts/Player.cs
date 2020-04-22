using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour
    {
        public float Speed;
        private float bottomBound;
        private float leftBound;
        private Camera mainCamera;
        private GameObject player;
        private float rightBound;
        private Vector2 screenBounds;
        private float screenHeight;
        private float topBound;

        public void MoveDown()
        {
            MovePlayer(0, -Speed);
        }

        public void MoveLeft()
        {
            MovePlayer(-Speed, 0);
        }

        public void MoveRight()
        {
            MovePlayer(Speed, 0);
        }

        public void MoveUp()
        {
            MovePlayer(0, Speed);
        }

        public void MoveVector(Vector2 vector)
        {
            MovePlayer(vector.x, vector.y);
        }

        private bool CheckPosition(float xShift, float yShift)
        {
            if (player.transform.position.x + xShift > rightBound ||
                player.transform.position.x + xShift < leftBound ||
                player.transform.position.y + yShift > topBound)
            {
                return true;
            }

            return false;
        }

        private void MovePlayer(float xShift, float yShift)
        {
            if (CheckPosition(xShift, yShift))
            {
                return;
            }

            player.transform.position = new Vector3(player.transform.position.x + xShift, player.transform.position.y + yShift, player.transform.position.z);
        }

        private void RefreshBounds()
        {
            screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
            topBound = screenBounds.y;
            bottomBound = topBound - screenHeight;
        }

        private void Start()
        {
            player = gameObject;
            mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
            screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
            topBound = screenBounds.y;
            bottomBound = -screenBounds.y;
            leftBound = -screenBounds.x;
            rightBound = screenBounds.x;
            screenHeight = topBound * 2;
#if UNITY_ANDROID
            Speed *= 0.1f;
#endif
        }

        private void Update()
        {
            RefreshBounds();
            if (player.transform.position.y < bottomBound)
            {
                player.transform.position = new Vector3(player.transform.position.x, bottomBound, player.transform.position.z);
            }
        }
    }
}
