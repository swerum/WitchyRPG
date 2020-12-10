using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObject : ClickableItem
{
    [SerializeField] GameObject activatableObject = null;

    public override void LeftClick()
    {
        activatableObject.SetActive(!activatableObject.activeSelf);
    }


}
