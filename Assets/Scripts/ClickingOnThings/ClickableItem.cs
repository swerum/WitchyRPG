using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//parent class that is clicked on to some effect.
public class ClickableItem : MonoBehaviour
{
    [SerializeField] bool canPlaceObjectOnThis = false;
    public bool CanPlaceObjectOnThis { get { return canPlaceObjectOnThis; } }

    public virtual void LeftClick()
    {

    }

    public virtual void RightClick()
    {

    }
}
