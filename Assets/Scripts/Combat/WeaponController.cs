using System.Collections;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject Axe; //add axe game object in inspector
    private bool CanAttack = true; // flag to check if player can attack
    public bool isAttacking = false; // flag to indicate if an attack is in progress
    public float attackCooldown = 1.0f; // cooldown time between attacks
   // public AudioClip attackSound; // sound to play on attack

    private void Update()
    {
        if (Time.timeScale == 0f)
            return; 

        // Check for attack input (left mouse button)
        if (Input.GetMouseButtonDown(0) && CanAttack)
        {
            Attack();
        }
    }

    private void Attack()
    {
        isAttacking = true; // set attacking flag
        CanAttack = false; // disable attacking 
        // play attack animation
        Animator anim = Axe.GetComponent<Animator>();
        anim.SetTrigger("Attack");
      //  AudioSource ac = GetComponent<AudioSource>();
      //  ac.PlayOneShot(attackSound); // play attack sound
        StartCoroutine(ResetCooldown()); // start cooldown coroutine
    }

    IEnumerator ResetCooldown()
    {
        StartCoroutine(ResetAttackBool()); // start coroutine to reset attacking flag
        yield return new WaitForSeconds(attackCooldown); // wait for cooldown duration
        CanAttack = true; // enable attacking
    }
    IEnumerator ResetAttackBool()
    {
        yield return new WaitForSeconds(1.0f); // wait for cooldown duration
        isAttacking = false; // reset attacking flag
    }
}



