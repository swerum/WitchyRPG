﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class NPCSpeech : MonoBehaviour
{
    [SerializeField] string[] text = new string[2];
    [SerializeField] bool[] playerSaysText = new bool[2];
    [SerializeField] Sprite face;
    [SerializeField] string npcName = "npc";

    SpeechInfo[] speechInfos;
    public SpeechInfo[] SpeechInfos {  get { return speechInfos; } }

    private void Start()
    {
        speechInfos = new SpeechInfo[text.Length];
        for (int i = 0; i< text.Length; i++)
        {
            if (playerSaysText[i]) {
                speechInfos[i] = new SpeechInfo(null, text[i], "Player", true);
            } else
            {
                speechInfos[i] = new SpeechInfo(face, text[i], npcName, false);
            }
        }
    }
}
