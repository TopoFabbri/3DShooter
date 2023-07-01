using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField] private Id[] stateIds;
    private readonly Dictionary<Id, Action> updatesById = new();
    private Id currentState;
    private bool isInitialized;

    private void Awake()
    {
        if (!isInitialized)
            Initialize();
    }

    private void Update()
    {
        updatesById[currentState]?.Invoke();
    }

    /// <summary>
    /// Subscribe method to action
    /// </summary>
    /// <param name="stateId"></param>
    /// <param name="updateMethod"></param>
    public void Subscribe(Id stateId, Action updateMethod)
    {
        if (!isInitialized)
            Initialize();

        if (updatesById.ContainsKey(stateId))
        {
            updatesById[stateId] += updateMethod;
        }
    }
    
    /// <summary>
    /// Unsubscribe method from action
    /// </summary>
    /// <param name="stateId"></param>
    /// <param name="updateMethod"></param>
    public void UnSubscribe(Id stateId, Action updateMethod)
    {
        if (updatesById.ContainsKey(stateId))
            updatesById[stateId] -= updateMethod;
    }

    /// <summary>
    /// Set current state
    /// </summary>
    /// <param name="stateId"></param>
    public void ChangeState(Id stateId)
    {
        if(updatesById.ContainsKey(stateId))
        {
            currentState = stateId;
        }
    }

    /// <summary>
    /// Initialize state machine
    /// </summary>
    private void Initialize()
    {
        foreach (var id in stateIds)
        {
            updatesById.Add(id, delegate { });
        }

        if(stateIds.Any())
        {
            currentState = stateIds.First();
        }

        isInitialized = true;
    }
}
