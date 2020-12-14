using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObject : ClickableItem
{
    [SerializeField] GameObject activatableObject = null;

    /// <summary>
    /// Activates and deactivates a specified object.
    /// </summary>
    public override void LeftClick()
    {
        activatableObject.SetActive(!activatableObject.activeSelf);
    }


}
