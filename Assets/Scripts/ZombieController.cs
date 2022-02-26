using System.Collections;
using UnityEngine;

public class ZombieController : MonoBehaviour, ICanTakeDamage
{
    public int HitPoints = 1;
    public int ScorePoints = 10;
    public float Speed = 0.5f;
    public float ChaseRange = 2;
    public int Damage = 1;
    public float DamageCooldown = 5f;

    public GameObject player;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator anim;
    private bool isChasing = false;
    private bool canAttack = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // Checks if player is within chase range..
        isChasing = Vector2.Distance(player.transform.position, rb.transform.position) < ChaseRange;

        if (isChasing)
        {
            // Rotates towards player and starts moving..
            Vector3 direction = player.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
            direction.Normalize();
            movement = direction;
        }

        anim.SetBool("IsChasing", isChasing);
    }

    private void FixedUpdate()
    {
        rb.MovePosition((Vector2)transform.position + (movement * Speed * Time.deltaTime));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var dmgComponent = collision.gameObject.GetComponent<ICanTakeDamage>();
        if (dmgComponent != null)
        {
            if (!canAttack)
                return;

            anim.SetBool("IsAttacking", true);
            dmgComponent.TakeDamage(Damage);
            AttackCooldown();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        var dmgComponent = collision.gameObject.GetComponent<ICanTakeDamage>();
        if (dmgComponent != null)
        {
            anim.SetBool("IsAttacking", false);
        }
    }

    public void TakeDamage(int damage)
    {
        HitPoints -= damage;
        if (HitPoints <= 0)
        {
            ScoreManager.AddScorePoints(ScorePoints);
            gameObject.SetActive(false);
            Destroy(gameObject, 1f);
        }
    }

    public IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(DamageCooldown);
        canAttack = true;
    }
}