using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectedMovement : MonoBehaviour
{

    [SerializeField] float walkSpeed = 5;
    [SerializeField] float pauseTime = 1f;

    bool headedSomewhere = false;
    float time = -1f;
    Vector2 currentDestination;
    Queue<Vector2> destinations = new Queue<Vector2>();


    void Update()
    {
        if (CheckAreMoving()) MoveTowardsDestination();

    }

    /// <summary>
    /// Add a new destination to the destinations queue
    /// </summary>
    /// <param name="destination"></param>
    public void AddDestination(Vector2 destination) { destinations.Enqueue(destination); headedSomewhere = true; }


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
                //get next position
                if (destinations.Count == 0) { headedSomewhere = false; }
                else
                {
                    time = 0f;
                    currentDestination = destinations.Dequeue();
                }
            }
        }
    }

}