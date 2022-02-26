using UnityEngine;

public class ShootController : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    public Animator animator;

    private bool isShooting = false;
    private bool isReloading = false;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (isShooting)
                return;

            isShooting = true;

            animator.SetBool("Shooting", isShooting);

            var bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);

            isShooting = false;

            animator.SetBool("Shooting", isShooting);
        }
        else if(Input.GetKeyDown(KeyCode.R))
        {
            if (isReloading)
                return;


            animator.SetBool("Reloading", true);
            Invoke(nameof(SetReloadingAnimation), 0.5f);
        }
    }


    protected void SetReloadingAnimation()
    {
        animator.SetBool("Reloading", false);
    }
}