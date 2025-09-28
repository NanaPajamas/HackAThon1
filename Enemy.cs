using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    public HealthBar healthBar;
    public float moveSpeed = 3.5f;
    public float aggroRange = 10f;
    public float attackRange = 2f;
    public int attackDamage = 10;
    public float attackCooldown = 1.5f; // Time between attacks

    private NavMeshAgent agent;
    private Transform target;
    private bool targetLocked;
    private float lastAttackTime;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;

        target = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (target != null)
        {
            float dist = Vector3.Distance(agent.transform.position, target.position);
            SetTarget(target, dist);
        }
    }

    void Update()
    {
        if (target == null) return;

        float dist = Vector3.Distance(agent.transform.position, target.position);
        SetTarget(target, dist);

        if (dist <= attackRange)
        {
            Attack();
        }
    }

    private void SetTarget(Transform target, float distance)
    {
        if (distance <= aggroRange)
        {
            targetLocked = true;
            agent.SetDestination(target.position);
        }
        else
        {
            targetLocked = false;
        }
    }

    private void Attack()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            lastAttackTime = Time.time;

            // Assuming the player has a "PlayerHealth" or similar component
            PlayerController player = target.GetComponent<PlayerController>();
            if (player != null)
            {
                player.playerHealth.TakeDamage(attackDamage);
                Debug.Log($"Enemy attacked player for {attackDamage} damage!");
            }
        }
    }
}
