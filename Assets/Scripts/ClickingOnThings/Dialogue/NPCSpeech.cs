using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchyRPG.DialogueSystem
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class NPCSpeech : ClickableItem
    {
        [Tooltip("The dialogue that is said.")]
        [SerializeField] string[]   text            = new string[2];
        [Tooltip("For each set of text in Text[], was this said by the player?")]
        [SerializeField] bool[]     playerSaysText  = new bool[2];
        [Tooltip("The sprite that is displayed as this NPC's face on the text box.")]
        [SerializeField] Sprite     face            = null;
        [Tooltip("The name that is displayed on the textbox as this npc's name.")]
        [SerializeField] string     npcName         = "npc";

        SpeechInfo[] speechInfos;
        public SpeechInfo[] SpeechInfos { get { return speechInfos; } }

        //configure speechinfo settings
        private void Start()
        {
            SetSpeechInfoArray();
        }

        /// <summary>
        /// Turns the npc and dialogue information and turns it into a speechInfo array.
        /// </summary>
        private void SetSpeechInfoArray()
        {
            speechInfos = new SpeechInfo[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                if (playerSaysText[i])
                {
                    speechInfos[i] = new SpeechInfo(null, text[i], "Player", true);
                }
                else
                {
                    speechInfos[i] = new SpeechInfo(face, text[i], npcName, false);
                }
            }
        }

        public override void LeftClick()
        {
            PlayerTalk.Instance.StartDialogue(this);
        }
    }
}