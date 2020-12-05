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
        }
    }
}
