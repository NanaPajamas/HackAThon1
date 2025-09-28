using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;               // Projectile movement speed
    public float damage = 10f;              // Damage dealt to enemy
    public float lifetime = 5f;             // Lifetime before destroying projectile
    public LayerMask enemyLayer;            // Layer for enemies

    private void Start()
    {
        // Destroy the projectile after its lifetime expires
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        // Move the projectile forward every frame
        transform.Translate(speed * Time.deltaTime * Vector3.forward);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object is on the enemy layer
        if (((1 << other.gameObject.layer) & enemyLayer) != 0)
        {
            // Try to get the Health component on the enemy
            HealthBar enemyHealth = other.GetComponent<HealthBar>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage((int)damage);
            }

            // Destroy projectile on hitting enemy
            Destroy(gameObject);
        }
        else
        {
            // Optional: Destroy projectile if it hits anything else
            Destroy(gameObject);
        }
    }
}
