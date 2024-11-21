using UnityEngine;

public class BulletDebug : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Bullet {gameObject.name} collided with: {collision.gameObject.name} at position {transform.position}");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Bullet {gameObject.name} entered trigger of: {other.gameObject.name} at position {transform.position}");
    }

    private void OnDestroy()
    {
        Debug.Log($"Bullet {gameObject.name} destroyed at position: {transform.position}");
    }
}
