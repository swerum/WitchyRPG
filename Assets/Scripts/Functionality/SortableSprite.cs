using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortableSprite : MonoBehaviour
{
    //sorts sprite by y value to create illusion of depth.
    SpriteRenderer sr;
    void Start()  { sr = GetComponent<SpriteRenderer>();  }
    void Update() { sr.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1; }
}
