using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//a class containing commonly used functions
public class Utility
{
    /// <summary>
    /// Get distance between two objects
    /// </summary>
    /// <param name="t1"></param>
    /// <param name="t2"></param>
    /// <returns></returns>
    public static float GetDistance(Transform t1, Transform t2)
    {
        float xDist = Mathf.Pow(t1.position.x - t2.position.x, 2);
        float yDist = Mathf.Pow(t1.position.y - t2.position.y, 2);
        return Mathf.Sqrt(xDist + yDist);
    }

    /// <summary>
    /// Checks in what x direction this NPC should go.
    /// </summary>
    /// <param name="pos1">The position of the object in question</param>
    /// <param name="pos2">The position of our destination</param>
    /// <param name="speed">The speed at which the object is moving</param>
    /// <returns>returns 0 if npc should not move. 1 if they should go right. -1 if they should go left.</returns>
    public static int GetXDirection(Vector2 pos1, Vector2 pos2, float speed)
    {
        if (pos1.x > pos2.x + speed) { return -1; }
        else if (pos1.x < pos2.x - speed) { return 1; }
        else return 0;
    }

    /// <summary>
    /// Checks in what y direction this NPC should go.
    /// </summary>
    /// <param name="pos1">The position of the object in question</param>
    /// <param name="pos2">The position of our destination</param>
    /// <param name="speed">The speed at which the object is moving</param>
    /// <returns>returns 0 if npc should not move. 1 if they should go up. -1 if they should go down.</returns>
    public static int GetYDirection(Vector2 pos1, Vector2 pos2, float speed)
    {
        if (pos1.y > pos2.y + speed) { return -1; }
        else if (pos1.y < pos2.y - speed) { return 1; }
        else return 0;
    }

    /// <summary>
    /// takes a 2d position and snaps it to a grid with its gridsize is 1
    /// </summary>
    /// <param name="pos">The position you want to snap</param>
    /// <returns>The closest position on the grid</returns>
    public static Vector2 SnapToGrid(Vector2 pos)
    {
        return new Vector2(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y));
    }

}
