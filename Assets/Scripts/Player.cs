using System;
using Assets.Scripts.Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Player : MovingObject
    {
        public int Health;
        public Rigidbody2D projectile;
        public float ProjectileSpawnTime;
        public float Speed;
        private readonly Vector2 projectile1Vector = new Vector2(0, 0.2f);
        private readonly Vector2 projectile2Vector = new Vector2(-0.06f, 0.2f);
        private readonly Vector2 projectile3Vector = new Vector2(0.06f, 0.2f);
        private Rigidbody2D body;
        private Text healthText;
        private GameObject player;

        public void MoveVector(Vector2 vector)
        {
            var moveVector = vector;
            if (IsNextPositionOutOfScreenX(vector.x))
            {
                moveVector -= new Vector2(vector.x, 0);
            }

            if (IsNextPositionOutOfScreenY(vector.y))
            {
                moveVector -= new Vector2(0, vector.y);
            }

            // Vector3 velocity = Vector3.zero;
            // Vector3 desiredPosition = player.transform.position + new Vector3(xShift, yShift, 0);
            // Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, 0.01f);
            // player.transform.position = smoothPosition;

            body.MovePosition(body.position + moveVector);
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
            if (collideObject.layer == Constants.EnemyProjectileLayer)
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

        private void SpawnProjectile(Vector2 projectileVector)
        {
            var instance = Instantiate(projectile, gameObject.GetComponent<Transform>().position + new Vector3(0, 0, Constants.ProjectileZShift), Converters.GetAngleFromDirection(projectileVector.x, projectileVector.y));
            var newProjectile = instance.gameObject.GetComponent<PlayerProjectile>();
            newProjectile.xSpeed = projectileVector.x;
            newProjectile.ySpeed = projectileVector.y;
        }

        private void SpawnProjectiles()
        {
            SpawnProjectile(projectile1Vector);
            SpawnProjectile(projectile2Vector);
            SpawnProjectile(projectile3Vector);
        }

        private new void Start()
        {
            base.Start();
            player = gameObject;
            body = player.GetComponent<Rigidbody2D>();
            body.freezeRotation = true;
            body.velocity = Vector2.zero;
            healthText = GameObject.Find(Constants.HealthTextName).GetComponent<Text>();
            healthText.text = Health.ToString();
            InvokeRepeating("SpawnProjectiles", 0f, ProjectileSpawnTime);
#if UNITY_ANDROID
            Speed *= 0.1f;
#endif
        }

        private new void Update()
        {
            base.Update();
            RefreshHealth();
            //CheckBottomBound();
        }
    }
}
