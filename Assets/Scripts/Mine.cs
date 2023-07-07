using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] private string characterTag = "Character";
    [SerializeField] private GameObject explosion;

    private void Explode()
    {
        var trans = transform;
        Instantiate(explosion, trans.position, trans.rotation);
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(characterTag))
            Explode();
    }
}
