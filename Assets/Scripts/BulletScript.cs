using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public int damage = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var dmgComponent = collision.gameObject.GetComponent<ICanTakeDamage>();
        if (dmgComponent != null)
            dmgComponent.TakeDamage(damage);

        gameObject.SetActive(false);
        Destroy(gameObject, 0.25f);
    }
}