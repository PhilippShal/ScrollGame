using System;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public static class Converters
    {
        public static quaternion GetAngleFromDirection(float x, float y)
        {
            float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
            return Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
