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
        // Aquí puedes poner lógica para dañar enemigos, etc.
        Destroy(gameObject);
    }
}
