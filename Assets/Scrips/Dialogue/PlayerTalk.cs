using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerTalk : MonoBehaviour
{
    [SerializeField] GameObject textBoxPrefab;
    [SerializeField] Sprite face;
    [SerializeField] string playerName = "Player";
    TextBox currentTextBox = null;

    List<NPCSpeech> npcs = new List<NPCSpeech>();
    Transform canvas;

    private void Start() { canvas = GameObject.FindGameObjectWithTag("Canvas").transform; }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (currentTextBox == null)
            {
                NPCSpeech npc = GetClosestNPC();
                if (npc == null) return;
                currentTextBox = Instantiate(textBoxPrefab).GetComponent<TextBox>();
                currentTextBox.SetTextBoxVariables(ConfigureSpeechInfos(npc.SpeechInfos), canvas);
            } else { currentTextBox.PlayNextText(); }
        }
    }

    private NPCSpeech GetClosestNPC()
    {
        if (npcs.Count == 0) return null;
        NPCSpeech closest = null;
        float closestDist = float.MaxValue;
        foreach (NPCSpeech npc in npcs)
        {
            float npcDist = Utility.GetDistance(npc.transform, transform);
            if (npcDist < closestDist)
            {
                closest = npc;
                closestDist = npcDist;
            }
        }
        return closest;
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

    #region keep track of nearby npcs
    private void OnTriggerEnter2D(Collider2D collision)
    {
        NPCSpeech npc = collision.gameObject.GetComponent<NPCSpeech>();
        if (npc != null) npcs.Add(npc);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        NPCSpeech npc = collision.gameObject.GetComponent<NPCSpeech>();
        if (npc != null) npcs.Remove(npc);
    }
    #endregion
}
