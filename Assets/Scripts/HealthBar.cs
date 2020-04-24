using System;
using Assets.Scripts.Helpers;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Transform bar;

    public void SetSize(float sizeNormalized)
    {
        bar.localScale = new Vector3(sizeNormalized, 1f);
    }

    // Start is called before the first frame update
    private void Start()
    {
        bar = transform.Find(Constants.BarName);
    }
}
