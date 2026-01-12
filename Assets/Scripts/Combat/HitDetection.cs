using UnityEngine;

public class HitDetection : MonoBehaviour
{
    // reference to the weapon controller to access attack state
    public WeaponController weapon;
    [SerializeField] private LayerMask enemyLayer; // layer mask to filter enemy objects
    [SerializeField] private int damageAmount = 25; // amount of damage to deal

    private void OnTriggerEnter(Collider other)
    {
        // if the other object is on the enemy layer and the weapon is attacking
        if ((((1 << other.gameObject.layer) & enemyLayer) != 0) && weapon.isAttacking)
        {
            Debug.Log("Hit detected on: " + other.gameObject.name);

            // trigger hit animation
            Animator enemyAnimator = other.GetComponent<Animator>();
            if (enemyAnimator != null)
            {
                enemyAnimator.SetTrigger("Hit");
            }

            // deal damage to the enemy
            EnemyManager enemyManager = other.GetComponent<EnemyManager>();
            if (enemyManager != null)
            {
                enemyManager.TakeDamage(damageAmount);
                Debug.Log("Dealt " + damageAmount + " damage to " + other.gameObject.name);
            }
            else
            {
                Debug.LogWarning("EnemyManager component not found on " + other.gameObject.name);
            }
        }
    }
}