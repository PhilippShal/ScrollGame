using System;
using Assets.Scripts;
using Assets.Scripts.Helpers;
using UnityEngine;

public class Enemy : MovingObject
{
    public int Health;
    public Rigidbody2D projectile;
    public float ProjectileSpawnTime;
    public float XMovement;
    public float XProjectileSpeed;
    public float YMovement;
    public float YProjectileSpeed;
    private Rigidbody2D body;

    private HealthBar healthBar;
    private int initialHealth;

    private void Death()
    {
        Destroy(gameObject);
    }

    private void DestroyIfOutOfScreen()
    {
        if (IsPositionOutOfScreen())
        {
            Destroy(gameObject);
        }
    }

    private void Move()
    {
        body.MovePosition(body.position + new Vector2(XMovement, YMovement));
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        var collideObject = collider.gameObject;
        if (collideObject.layer == Constants.PlayerProjectileLayer)
        {
            Health -= collideObject.GetComponent<PlayerProjectile>().Damage;
        }

        if (Health <= 0)
        {
            Death();
        }
    }

    private void SpawnProjectile()
    {
        var instance = Instantiate(projectile, gameObject.GetComponent<Transform>().position + new Vector3(0, 0, Constants.ProjectileZShift), Converters.GetAngleFromDirection(XProjectileSpeed, YProjectileSpeed));
        var newProjectile = instance.gameObject.GetComponent<EnemyProjectile>();
        newProjectile.xSpeed = XProjectileSpeed;
        newProjectile.ySpeed = YProjectileSpeed;
    }

    private new void Start()
    {
        base.Start();
        InvokeRepeating("SpawnProjectile", 0f, ProjectileSpawnTime);
        healthBar = gameObject.GetComponentInChildren<HealthBar>();
        body = gameObject.GetComponent<Rigidbody2D>();
        initialHealth = Health;
    }

    private new void Update()
    {
        base.Update();
        healthBar.SetSize(Health / (float)initialHealth);
        Move();
        DestroyIfOutOfScreen();
    }
}
