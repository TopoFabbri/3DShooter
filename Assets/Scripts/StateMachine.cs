using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField] private Id[] stateIds;
    private Dictionary<Id, Action> updatesById = new();
    private Id currentState;

    private void Awake()
    {
        foreach (var id in stateIds)
        {
            updatesById.Add(id, delegate { });
        }

        if(stateIds.Any())
        {
            currentState = stateIds.First();
        }
    }

    private void Update()
    {
        updatesById[currentState]?.Invoke();
    }

    public void Subscribe(Id stateId, Action updateMethod)
    {
        if (updatesById.ContainsKey(stateId))
        {
            updatesById[stateId] += updateMethod;
        }
    }
    
    public void UnSubscribe(Id stateId, Action updateMethod)
    {
        if (updatesById.ContainsKey(stateId))
            updatesById[stateId] -= updateMethod;
    }

    public void ChangeState(Id stateId)
    {
        if(updatesById.ContainsKey(stateId))
        {
            currentState = stateId;
        }
    }
}
