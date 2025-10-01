using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Spawning
{
    public static Vector2 RandomOnCircle(Vector2 center, float r)
    {
        float theta = Random.Range(0f, 2f * Mathf.PI);
        return center + new Vector2(Mathf.Cos(theta), Mathf.Sin(theta)) * r;
    }
}
