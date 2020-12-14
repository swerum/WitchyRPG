using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WitchyRPG.DialogueSystem
{
    public class TextBox : MonoBehaviour
    {
        [Tooltip("Where the textbox should be on the screen.")]
        [SerializeField] Vector2 position = Vector2.zero;
        [Header("Child Components")]
        [SerializeField] Image image = null;
        [SerializeField] Text speechText = null;

        SpeechInfo[] speechInfos;
        int counter = 0;

        /// <summary>
        /// A sort of constructor for this gameobject
        /// </summary>
        /// <param name="speechInfos">An array of speechInfos specifying the dialogue.</param>
        /// <param name="canvas">The parent Transform canvas: The text box's parent.</param>
        public void SetTextBoxVariables(SpeechInfo[] speechInfos, Transform canvas)
        {
            if (canvas == null) { canvas = GameObject.FindGameObjectWithTag("Canvas").transform; }
            transform.SetParent(canvas);
            GetComponent<RectTransform>().anchoredPosition = position;
            this.speechInfos = speechInfos;
            PlayNextText();
        }

        /// <summary>
        /// If there is a next text, it is played. If not, the text box is destroyed.
        /// </summary>
        public void PlayNextText()
        {
            if (counter >= speechInfos.Length)
            {
                Destroy(gameObject);
                return;
            }
            SpeechInfo speechInfo = speechInfos[counter];
            image.sprite = speechInfo.face;
            speechText.text = speechInfo.text;
            counter++;
        }
    }
}