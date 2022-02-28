using System.Collections;
using UnityEngine;

public class ZombieController : MonoBehaviour, ICanTakeDamage
{
    public int HitPoints = 1;
    public int ScorePoints = 10;
    public float Speed = 0.5f;
    public float ChaseRange = 2;
    public float AttackRange = 0.4f;
    public int Damage = 5;
    public float DamageCooldown = 3f;

    public GameObject player;
    public ScoreManager scoreManager;

    public AudioClip zombieIdleAudio;
    public AudioClip zombieGrowlAudio;
    public AudioSource audioSrc;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator anim;
    

    private bool canAttack = true;
    private bool isTakingDamage = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        var distanceToPlayer = Vector2.Distance(player.transform.position, rb.transform.position);
        // Checks if player is within chase range..
        var canChasePlayer = distanceToPlayer < ChaseRange;
        var canAttackPlayer = distanceToPlayer <= AttackRange;

        if (canChasePlayer)
        {
            // Rotates towards player and starts moving..
            Vector3 direction = player.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
            direction.Normalize();
            movement = direction;

            // Checks if player is close enough to attack..
            if (canAttackPlayer)
            {
                var dmgComponent = player.gameObject.GetComponent<ICanTakeDamage>();
                if (dmgComponent != null)
                {
                    if (!canAttack)
                        return;

                    anim.SetBool("IsAttacking", true);
                    canAttack = false;
                    dmgComponent.TakeDamage(Damage);
                    StartCoroutine(AttackCooldown());
                }
            }
            else
            {
                anim.SetBool("IsAttacking", false);
            }
        }

        if (!audioSrc.isPlaying)
        {
            // Use some random value to avoid spamming sounds..
            if (Random.value <= 0.05)
            {
                // Checks if player is close enough to play a growl or idle audio..
                audioSrc.pitch = Random.Range(1f, 3f);
                audioSrc.PlayOneShot(canAttackPlayer ? zombieGrowlAudio : zombieIdleAudio);
            }
        }

        anim.SetBool("IsChasing", canChasePlayer);
    }

    private void FixedUpdate()
    {
        rb.MovePosition((Vector2)transform.position + (movement * Speed * Time.deltaTime));
    }

    public void TakeDamage(int damage)
    {
        if (isTakingDamage)
            return;

        isTakingDamage = true;
        HitPoints -= damage;
        if (HitPoints <= 0)
        {
            scoreManager.AddScorePoints(ScorePoints);
            gameObject.SetActive(false);
            Destroy(gameObject, 1f);
        }
        isTakingDamage = false;
    }

    public IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(DamageCooldown);
        canAttack = true;
    }
}