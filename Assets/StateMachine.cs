using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public event Action onGameplayUpdate;
    public event Action onPauseUpdate;
    [SerializeField] Id[] stateIds;
    private Dictionary<Id, Action> updatesById;
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

    void Update()
    {
        updatesById[currentState]?.Invoke();
    }

    public void Suscribe(Id stateId, Action UpdateMethod)
    {
        if (updatesById.ContainsKey(stateId))
        {
            updatesById[stateId] += UpdateMethod;
        }
    }

    public void ChangeState(Id stateId)
    {
        if(updatesById.ContainsKey(stateId))
        {
            currentState = stateId;
        }
    }
}
