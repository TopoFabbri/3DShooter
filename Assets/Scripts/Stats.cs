using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] private float hp = 100f;
    [SerializeField] private bool isCharacter;
    [SerializeField] private Transform life;
    [SerializeField] private Hud hud;
    [SerializeField] private Id stateId;
    [SerializeField] private StateMachine stateMachine;

    private const float LifeRegen = .5f;
    
    public delegate void ObjectDestroyed(GameObject destroyed);
    public static event ObjectDestroyed DestroyedEvent;
    
    private void OnEnable()
    {
        if (!stateMachine)
            stateMachine = FindObjectOfType<StateMachine>();
        
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
        if (hp <= 0)
            Die();

        if (isCharacter)
        {
            hp += LifeRegen * Time.deltaTime;
            hud?.SetSlider(hp);
        }   
        
        hp = Mathf.Clamp(hp, 0f, 100f);

        if (!life) return;

        var lifeTrans = life.transform;
        var scale = lifeTrans.localScale;
        
        scale.x = hp / 1000f;
        lifeTrans.localScale = scale;
    }

    /// <summary>
    /// Damage this object
    /// </summary>
    /// <param name="damage"></param>
    public void LoseLife(float damage)
    {
        hp -= damage;
        
        if (LifeRegen > 0)
            hud?.SetSlider(hp);
    }

    /// <summary>
    /// Destroy object
    /// </summary>
    private void Die()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        DestroyedEvent?.Invoke(gameObject);
    }
}