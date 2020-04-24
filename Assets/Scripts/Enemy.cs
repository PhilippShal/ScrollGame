using System;
using Assets.Scripts;
using Assets.Scripts.Helpers;
using UnityEngine;

public class Enemy : MovingObject
{
    public int Health;
    public Rigidbody2D projectile;
    public float ProjectileSpawnTime;
    public float xProjectileSpeed;
    public float yProjectileSpeed;

    private HealthBar healthBar;
    private int initialHealth;

    private void SpawnProjectile()
    {
        var instance = Instantiate(projectile, gameObject.GetComponent<Transform>().position + new Vector3(0, 0, Constants.ProjectileZShift), Converters.GetAngleFromDirection(xProjectileSpeed, yProjectileSpeed));
        var newProjectile = instance.gameObject.GetComponent<EnemyProjectile>();
        newProjectile.xSpeed = xProjectileSpeed;
        newProjectile.ySpeed = yProjectileSpeed;
    }

    private new void Start()
    {
        base.Start();
        InvokeRepeating("SpawnProjectile", 0f, ProjectileSpawnTime);
        healthBar = gameObject.GetComponentInChildren<HealthBar>();
        initialHealth = Health;
    }

    private new void Update()
    {
        base.Update();
        healthBar.SetSize(Health / (float)initialHealth);
    }
}
