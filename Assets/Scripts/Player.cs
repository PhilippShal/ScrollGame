using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Player : MovingObject
    {
        public int Health;
        public float Speed;
        private Rigidbody2D body;
        private Text healthText;
        private GameObject player;

        public void MoveVector(Vector2 vector)
        {
            if (IsNextPositionOutOfScreen(vector.x, vector.y))
            {
                return;
            }

            // Vector3 velocity = Vector3.zero;
            // Vector3 desiredPosition = player.transform.position + new Vector3(xShift, yShift, 0);
            // Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, 0.01f);
            // player.transform.position = smoothPosition;

            body.MovePosition(body.position + vector);
        }

        private void CheckBottomBound()
        {
            if (body.position.y < bottomBound)
            {
                body.position = new Vector3(body.position.x, bottomBound, player.transform.position.z);
            }
        }

        private void Gameover()
        {
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            var collideObject = collider.gameObject;
            if (collideObject.layer == 9)
            {
                Health -= collideObject.GetComponent<EnemyProjectile>().Damage;
            }
        }

        private void RefreshHealth()
        {
            healthText.text = Health.ToString();
            if (Health <= 0)
            {
                Destroy(gameObject);
                Gameover();
            }
        }

        private new void Start()
        {
            base.Start();
            player = gameObject;
            body = player.GetComponent<Rigidbody2D>();
            body.freezeRotation = true;
            body.velocity = Vector2.zero;
            healthText = GameObject.Find("HealthText").GetComponent<Text>();
            healthText.text = Health.ToString();
#if UNITY_ANDROID
            Speed *= 0.1f;
#endif
        }

        private new void Update()
        {
            base.Update();
            RefreshHealth();
            CheckBottomBound();
        }
    }
}
