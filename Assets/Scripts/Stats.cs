using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] private bool isCharacter;
    [SerializeField] private Transform life;
    [SerializeField] private Hud hud;
    [SerializeField] private Id stateId;
    [SerializeField] private StateMachine stateMachine;
    [SerializeField] private float initialHp = 100f;
    [SerializeField] private LevelManager levelManager;

    private float hp;
    private const float LifeRegen = .5f;

    private void OnEnable()
    {
        hp = initialHp;
        
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
            if (hud)
                hud.SetSlider(hp);
        }

        hp = Mathf.Clamp(hp, 0f, initialHp);

        if (!life) return;

        var lifeTrans = life.transform;
        var scale = lifeTrans.localScale;

        scale.x = hp / (initialHp * 10f);
        lifeTrans.localScale = scale;
    }

    /// <summary>
    /// Damage this object
    /// </summary>
    /// <param name="damage"></param>
    public void LoseLife(float damage)
    {
        hp -= damage;

        if (isCharacter && hud)
            hud.SetSlider(hp);
    }

    /// <summary>
    /// Destroy object
    /// </summary>
    private void Die()
    {
        if (isCharacter)
        {
            levelManager.Lose();
            return;
        }      
        
        GetComponent<Barrel>()?.Explode();
        Destroy(gameObject);
    }
}