using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectedMovement : MonoBehaviour
{
    [Tooltip("How fast the goblin walks.")]
    [SerializeField] float walkSpeed = 5;
    [Tooltip("How long they pause at each FarmableTile")]
    [SerializeField] float pauseTime = 1f;

    bool headedSomewhere = false;
    float time = -1f;

    Vector2 currentDestination;
    Vector2 ogPosition;

    Goblin goblin;
    Queue<FarmableTile> farmableTiles = new Queue<FarmableTile>();

    private void Start()
    {
        ogPosition = transform.position;
        if (TryGetComponent(out Goblin g)) {
            goblin = g;
            farmableTiles = goblin.GetFarmableTileQueue();
            headedSomewhere = true;
            currentDestination = farmableTiles.Peek().transform.position;
        } else { Debug.LogError("No Helper Goblin Component found."); }
    }

    void Update()
    {
        if (CheckAreMoving()) MoveTowardsDestination();

    }

    /// <summary>
    /// Checks if this player should be moving and handles timer for pausing between going to new destination
    /// </summary>
    /// <returns> returns true, if the player should be moving</returns>
    private bool CheckAreMoving()
    {
        if (!headedSomewhere) return false; ;
        //check if we're pausing
        if (time != -1)
        {
            time += Time.deltaTime;
            if (time >= pauseTime) { time = -1; }
            return false;
        }
        return true;
    }

    /// <summary>
    /// Move towards destination first in x direction, then in y direction.
    /// </summary>
    private void MoveTowardsDestination()
    {
        int xDirection = Utility.GetXDirection(transform.position, currentDestination, walkSpeed);
        Vector2 pos = transform.position;
        if (xDirection != 0) { transform.position = new Vector2(pos.x + walkSpeed * xDirection, pos.y); }
        else
        {
            int yDirection = Utility.GetYDirection(transform.position, currentDestination, walkSpeed);
            if (yDirection != 0) { transform.position = new Vector2(pos.x, pos.y + walkSpeed * yDirection); }
            else
            {
                if (farmableTiles.Count == 0) { headedSomewhere = false; return; }
                //get next position
                time = 0f;
                FarmableTile farmableTile = farmableTiles.Dequeue();
                goblin.farmTile(farmableTile);
                if (farmableTiles.Count == 0) { currentDestination = ogPosition; return; }
                currentDestination = farmableTiles.Peek().transform.position;
            }
        }
    }

}