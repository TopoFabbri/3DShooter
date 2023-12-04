using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private GameObject[] rooms;
    [SerializeField] private GameObject[] changeRoomTriggers;
    [SerializeField] private StateMachine stateMachine;
    [SerializeField] private Id stateId;

    public static event Action<List<Transform>> OnRoomChanged;

    public int current { get; private set; }

    private void OnEnable()
    {
        stateMachine.Subscribe(stateId, OnUpdate);
    }

    private void OnDisable()
    {
        stateMachine.UnSubscribe(stateId, OnUpdate);
    }

    /// <summary>
    /// Gameplay-only update
    /// </summary>
    private void OnUpdate()
    {
        if (levelManager.EnemyCount <= 0 && current < changeRoomTriggers.Length)
            changeRoomTriggers[current].SetActive(true);
        else if (levelManager.EnemyCount <= 0)
            levelManager.ShowWinScreen();

    }

    /// <summary>
    /// Switch rooms
    /// </summary>
    /// <param name="newSpawns"></param>
    public void NextRoom(List<Transform> newSpawns)
    {
        if (current + 1 < rooms.Length)
            current++;

        rooms[current - 1].SetActive(false);
        rooms[current].SetActive(true);

        OnRoomChanged?.Invoke(newSpawns);
    }
}