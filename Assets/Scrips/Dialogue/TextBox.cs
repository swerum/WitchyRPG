using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WitchyRPG.DialogueSystem
{
    public class TextBox : MonoBehaviour
    {
        [SerializeField] Vector2 position = Vector2.zero;
        [Header("Child Components")]
        [SerializeField] Image image = null;
        [SerializeField] Text speechText = null;

        SpeechInfo[] speechInfos;
        int counter = 0;

        public void SetTextBoxVariables(SpeechInfo[] speechInfos, Transform canvas)
        {
            transform.SetParent(canvas);
            GetComponent<RectTransform>().anchoredPosition = position;
            this.speechInfos = speechInfos;
            PlayNextText();
        }

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