using System;
using Assets.Scripts.Helpers;
using UnityEngine;

public class PlayerProjectile : Projectile
{
    public int Damage;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == Constants.EnemyTag)
        {
            Destroy(gameObject);
        }
    }

    protected new void Start()
    {
        base.Start();
    }

    protected new void Update()
    {
        base.Update();
    }
}
