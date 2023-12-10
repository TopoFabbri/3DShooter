using System.Collections.Generic;
using UnityEngine;

namespace Level
{
    /// <summary>
    /// Change room on enter trigger
    /// </summary>
    public class ChangeRoomTrigger : MonoBehaviour
    {
        [SerializeField] private List<Transform> nextRoomSpawns = new();
        [SerializeField] private string characterTag = "Character";
        [SerializeField] private Transform character;
        [SerializeField] private GameObject sprite;
        [SerializeField] private float spriteHeight = 2f;

        private void Update()
        {
            sprite.transform.position = transform.position + Vector3.up * spriteHeight;
            sprite.transform.LookAt(character.position);
        }
    }
}
