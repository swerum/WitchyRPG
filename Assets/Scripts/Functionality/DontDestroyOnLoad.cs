using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// add this component to make an object carry over between scenes.
/// </summary>
public class DontDestroyOnLoad : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(this);
    }

}
