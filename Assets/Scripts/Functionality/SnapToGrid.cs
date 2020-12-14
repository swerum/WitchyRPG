using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToGrid : MonoBehaviour
{
    //snaps item to a grid (not during runtime)
    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying && this.transform.hasChanged)
            transform.position = Utility.SnapToGrid(transform.position);
    }

}
