using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemApplication { None, ProtectFromSound, HealPlayer};

[RequireComponent(typeof(SpriteRenderer))]
public class Items : MonoBehaviour
{
    public float price = 5f;
    public ItemApplication itemApplication;
    

}
