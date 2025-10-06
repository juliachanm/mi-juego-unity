using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform target;     // El personaje a seguir
    public float smoothSpeed = 0.125f; // Velocidad de movimiento suave
    public Vector3 offset;       // Ajuste de posición de la cámara

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Solo seguimos en X y Y. Z se mantiene para cámara 2D.
            transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
        }
    }
}

