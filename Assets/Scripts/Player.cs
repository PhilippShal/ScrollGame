using System;
using UnityEngine;
using UnityEngine.UI;

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
        private Rigidbody2D body;
        private bool obstacleCollision = false;
        public int health;
        private Text healthText;

        public void MoveVector(Vector2 vector)
        {
            if (CheckPosition(vector.x, vector.y))
            {
                obstacleCollision = false;
                return;
            }

            // Vector3 velocity = Vector3.zero;
            // Vector3 desiredPosition = player.transform.position + new Vector3(xShift, yShift, 0);
            // Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, 0.01f);
            // player.transform.position = smoothPosition;

            body.MovePosition(body.position + vector);
        }

        private bool CheckPosition(float xShift, float yShift)
        {
            if (body.position.x + xShift > rightBound ||
                body.position.x + xShift < leftBound ||
                body.position.y + yShift > topBound ||
                obstacleCollision)
            {
                return true;
            }

            return false;
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
            body = player.GetComponent<Rigidbody2D>();
            body.freezeRotation = true;
            mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
            screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
            topBound = screenBounds.y;
            bottomBound = -screenBounds.y;
            leftBound = -screenBounds.x;
            rightBound = screenBounds.x;
            screenHeight = topBound * 2;
            healthText = GameObject.Find("HealthText").GetComponent<Text>();
            healthText.text = health.ToString();
#if UNITY_ANDROID
            Speed *= 0.1f;
#endif
        }

        private void Update()
        {
            healthText.text = health.ToString();
            RefreshBounds();
            if (body.position.y < bottomBound)
            {
                body.position = new Vector3(body.position.x, bottomBound, player.transform.position.z);
            }
            
        }
    }
}
