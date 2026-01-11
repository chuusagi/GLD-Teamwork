using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class HitDetection : MonoBehaviour
{
    // reference to the weapon controller to access attack state
    public WeaponController weapon;
    [SerializeField] private LayerMask enemyLayer; // layer mask to filter enemy objects

    private void OnTriggerEnter(Collider other)
    {
        // if the other object is on the enemy layer and the weapon is attacking
        if ((((1 << other.gameObject.layer) & enemyLayer) != 0) && weapon.isAttacking)
        {
            Debug.Log("Hit detected on: " + other.gameObject.name);
            other.GetComponent<Animator>().SetTrigger("Hit");

            
        }
    }




}
