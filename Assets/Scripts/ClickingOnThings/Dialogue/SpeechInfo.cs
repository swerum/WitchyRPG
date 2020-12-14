using UnityEngine;

namespace WitchyRPG.DialogueSystem
{
    //dialogue information the textbox will need
    public struct SpeechInfo
    {
        public Sprite face;
        public string text;
        public string name;
        //isPlayer is used in order to assign the face and name of the player which is only stored in PlayerTalk.
        public bool isPlayer;

        public SpeechInfo(Sprite face, string text, string name, bool isPlayer)
        {
            this.face = face;
            this.text = text;
            this.name = name;
            this.isPlayer = isPlayer;
        }

    }
}
