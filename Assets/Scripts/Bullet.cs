using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 450f;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private GameObject particleSys;
    [SerializeField] private float damage = 50f;

    [FormerlySerializedAs("life")] [SerializeField]
    private float lifeTime = 5f;

    [SerializeField] private bool addPlayerVel;

    private Transform character;
    private const string CharacterObjectName = "Character";


    private void OnEnable()
    {
        if (!character)
            character = GameObject.Find(CharacterObjectName).GetComponentInChildren<Gun>().transform;
            
        rb.velocity = transform.forward * speed;

        if (addPlayerVel)
            rb.velocity += character.GetComponent<Rigidbody>().velocity;

        StartCoroutine(DestroyAfterTime(lifeTime));
    }

    /// <summary>
    ///  Deletes an object from map after given time
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    private IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        BulletManager.Instance.RecycleObject(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(particleSys, transform.position, Quaternion.identity);

        if (collision.gameObject.GetComponent<Stats>())
            collision.gameObject.GetComponent<Stats>().LoseLife(damage);
    }
}