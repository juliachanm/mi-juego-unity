using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float lifeTime = 2f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Aqu� puedes poner l�gica para da�ar enemigos, etc.
        Destroy(gameObject);
    }
}
