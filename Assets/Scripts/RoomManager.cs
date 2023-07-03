using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private GameObject[] rooms;
    
    private int current;

    public void NextRoom()
    {
        if (current + 1 > rooms.Length)
            current++;
     
        rooms[current - 1].SetActive(false);
        rooms[current].SetActive(true);
    }
}
