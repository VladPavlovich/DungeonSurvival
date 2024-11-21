using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject target;           
    public float speed = 3f;             
    public float attackCooldown = 1f;   
    public float attackDamage = 10f;     

    private Animator animator;           
    private float nextAttackTime = 0f;  
    private bool isPlayerInRange = false; 

    private PlayerHealth playerHealth;   

    private void Start()
    {
        animator = GetComponent<Animator>();
        Debug.Log("EnemyAI initialized.");

        if (target == null)
        {
            GameObject foundPlayer = GameObject.FindGameObjectWithTag("Player");
            if (foundPlayer != null)
            {
                target = foundPlayer;
            }
            else
            {
                Debug.LogWarning("No target assigned, and no GameObject with the 'Player' tag was found.");
            }
        }

        if (target != null)
        {
            playerHealth = target.GetComponent<PlayerHealth>();
            if (playerHealth == null)
            {
                Debug.LogError("No PlayerHealth component found on target");
            }
        }
    }

    private void Update()
    {
        if (target == null || playerHealth == null) return;

        // Check if the player is dead
        if (playerHealth.IsDead())
        {
            HandlePlayerDeath();
            return;
        }

        // Move toward player if not in attack range
        if (!isPlayerInRange)
        {
            MoveTowardTarget();
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isAttacking", true);

            // Attack if cooldown has elapsed
            if (Time.time >= nextAttackTime)
            {
                AttackPlayer();
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }

    private void MoveTowardTarget()
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        direction.y = 0;

        transform.position += direction * speed * Time.deltaTime;

        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

        Debug.Log("Moving toward target. Current position: " + transform.position);
    }

    private void AttackPlayer()
    {
        Debug.Log("Enemy is attacking!"); 

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
            Debug.Log("Damage applied to player."); 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            Debug.Log("Player entered attack range.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            animator.SetBool("isAttacking", false);
            Debug.Log("Player exited attack range.");
        }
    }

    private void HandlePlayerDeath()
    {
        // Stop animations
        animator.SetBool("isRunning", false);
        animator.SetBool("isAttacking", false);

        Debug.Log("Destroying enemy due to player death.");
        Destroy(gameObject);
    }
}
