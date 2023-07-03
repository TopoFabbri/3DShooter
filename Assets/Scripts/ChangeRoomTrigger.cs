using System.Collections.Generic;
using UnityEngine;

public class ChangeRoomTrigger : MonoBehaviour
{
    [SerializeField] private RoomManager roomManager;
    [SerializeField] private List<Transform> nextRoomSpawns = new();
    [SerializeField] private string characterTag = "Character";
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(characterTag)) 
            roomManager.NextRoom(nextRoomSpawns);
    }
}
