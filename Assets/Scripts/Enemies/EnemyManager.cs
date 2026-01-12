using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    // references 
    public NavMeshAgent agent;
    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer; // layer detection

    public float health; // enemy health

    // drop system
    public GameObject dropItem; // assign the item to drop in inspector

    //patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // combat
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    // detection+ states
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        // find the player object and get the NavMeshAgent component
        player = GameObject.Find("PlayerObj").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)
            Patroling();
        else if (playerInSightRange && !playerInAttackRange)
            ChasePlayer();
        else if (playerInAttackRange && playerInSightRange)
            AttackPlayer();
    }

    // patrolling behavior by moving to walk points defined in unity editor
    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint; // calculate distance to walkpoint

        // walkpoint reached -  if distance is less than 1, reset walkpoint
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    // calculates a random walk point within range and checks if it's on the ground
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        // ensure walkpoint is on the ground
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    // sets the agent's destination to the player's position to chase them
    private void ChasePlayer()
    {
        agent.SetDestination(player.position); // set destination to player position
    }

    // handles attacking the player by shooting a projectile and managing attack cooldown
    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            // check if projectile prefab is assigned before instantiating
            if (projectile != null)
            {
                // instantiate projectile and add force to it (aka shoot it)
                Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
                    rb.AddForce(transform.up * 8f, ForceMode.Impulse);
                }
            }
            else
            {
                Debug.LogWarning("Projectile prefab not assigned on " + gameObject.name);
            }

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    // resets the attack cooldown so the enemy can attack again
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    // reduces health by damage amount and destroys enemy if health is zero or less
    public void TakeDamage(int damage)
    {
        health -= damage;

        Debug.Log("Enemy Health: " + health); // debug to see health value

        if (health <= 0)
        {
            DestroyEnemy(); // call immediately instead of using Invoke
        }
    }

    // destroys the enemy game object and spawns drop
    private void DestroyEnemy()
    {
        Debug.Log("DestroyEnemy called for " + gameObject.name);

        // spawn drop item at enemy's position if assigned
        if (dropItem != null)
        {
            // spawn drop slightly above the enemy to prevent it from falling through the map
            Vector3 dropPosition = transform.position + Vector3.up * 0.5f;
            GameObject drop = Instantiate(dropItem, dropPosition, Quaternion.identity);
            Debug.Log("Drop spawned: " + drop.name + " at position " + dropPosition);
        }
        else
        {
            Debug.LogWarning("No drop item assigned to " + gameObject.name + "! Assign a drop prefab in the Inspector.");
        }

        Destroy(gameObject);
    }

    // draws gizmos in the editor to visualize attack and sight ranges
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}