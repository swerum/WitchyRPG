using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchyRPG.DialogueSystem
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerTalk : MonoBehaviour
    {
        [SerializeField] GameObject textBoxPrefab = null;
        [SerializeField] Sprite     face = null;
        [SerializeField] string     playerName = "Player";

        TextBox         currentTextBox = null;
        public TextBox TextBox { get { return currentTextBox; } }
        Transform       canvas;

        private void Start() { canvas = GameObject.FindGameObjectWithTag("Canvas").transform; }

        public void StartDialogue(NPCSpeech npc)
        {
            if (currentTextBox == null & npc != null)
            {
                currentTextBox = Instantiate(textBoxPrefab).GetComponent<TextBox>();
                currentTextBox.transform.SetParent(canvas);
                currentTextBox.SetTextBoxVariables(ConfigureSpeechInfos(npc.SpeechInfos), canvas);
            }
        }


        /// <summary>
        /// Configures the players text
        /// </summary>
        /// <param name="npc"></param>
        /// <returns></returns>
        private SpeechInfo[] ConfigureSpeechInfos(SpeechInfo[] speechInfos)
        {
            for (int i = 0; i < speechInfos.Length; i++)
            {
                if (speechInfos[i].isPlayer)
                {
                    speechInfos[i].face = this.face;
                    speechInfos[i].name = playerName;
                }
            }
            return speechInfos;
        }


        #region 
        private static PlayerTalk _instance;
        public static PlayerTalk Instance { get { return _instance; } }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }
        #endregion
    }

}