using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float damage = 10f;
    public float lifetime = 5f; // destroy projectile after 5 seconds if it doesn't hit anything

    private void Start()
    {
        // automatically destroy projectile after lifetime expires
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // check if projectile hit the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // try to get PlayerManager component and deal damage
            PlayerManager playerHealth = collision.gameObject.GetComponent<PlayerManager>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            // destroy projectile on contact with player
            Destroy(gameObject);
        }
        // also destroy on contact with ground or other objects 
        else if (collision.gameObject.layer != LayerMask.NameToLayer("EnemyLayer"))
        {
            Destroy(gameObject);
        }
    }
}