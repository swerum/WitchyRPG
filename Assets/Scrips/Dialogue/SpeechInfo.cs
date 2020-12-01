using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SpeechInfo 
{
    public Sprite face;
    public string text;
    public string name;
    public bool isPlayer;

    public SpeechInfo(Sprite face, string text, string name, bool isPlayer)
    {
        this.face = face;
        this.text = text;
        this.name = name;
        this.isPlayer = isPlayer;
    }

}
