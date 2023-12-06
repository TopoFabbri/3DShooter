using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator animator;
    [SerializeField] private StateMachine stateMachine;
    [SerializeField] private Id stateId;
    [SerializeField] private float animSpeed = 1f;
    private static readonly int State1 = Animator.StringToHash("State");

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
        var velocity = rb.velocity;
        var speed = Mathf.Sqrt(velocity.x * velocity.x + velocity.z * velocity.z);

        animator.SetInteger(State1, speed > animSpeed ? 1 : 0);
    }
}
