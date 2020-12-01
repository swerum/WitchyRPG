using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchyRPG.DialogueSystem
{
    public class NPCManager : MonoBehaviour
    {
        [SerializeField] List<DirectedMovement> npcs = new List<DirectedMovement>();
        void Start()
        {
            foreach (DirectedMovement npc in npcs)
            {
                npc.AddDestination(new Vector2(0, 1));
                npc.AddDestination(new Vector2(1, 1));
                npc.AddDestination(new Vector2(-1, 1));
            }
        }
    }
}
