using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BoxCheck
{
    public static bool Check(Vector2 pos, Vector2 size, int layer = 0)
    {
        return Physics2D.OverlapBox(pos, size, 0, layer);
    }
}
public static class RayCheck
{
    public static bool Check(Vector2 pos,Vector2 dir, float wallSlierSize, int layer = 0)
    {
        return Physics2D.Raycast(pos, dir, wallSlierSize, layer);
       
    }
}
