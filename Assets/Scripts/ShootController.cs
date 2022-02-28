using System.Collections;
using TMPro;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    public Animator animator;
    public TMP_Text txtAmmo;

    public AudioClip shotAudio;
    public AudioClip reloadAudio;

    private bool isShooting = false;
    private bool isReloading = false;

    private readonly int magazineSize = 15;
    private int currentAmmo = 15;
    private int remainingAmmo = 30;

    private AudioSource audioSrc;

    private void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        UpdateAmmoText();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            // If already shooting ignores the action...
            if (isShooting)
                return;

            // If magazine does not have bullets, ignores the action...
            if (currentAmmo <= 0)
                return;

            isShooting = true;
            animator.SetBool("Shooting", isShooting);

            var bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
            audioSrc.PlayOneShot(shotAudio);
            currentAmmo--;
            UpdateAmmoText();

            isShooting = false;
            animator.SetBool("Shooting", isShooting);
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            if (isReloading)
                return;

            ReloadingDelay();

            // Check if we need to reload and if we have ammo to do the reloading..
            if (currentAmmo <= magazineSize && remainingAmmo > 0)
            {
                // Calculates how many bullets we need to perform a full reload..
                var ammoToReload = magazineSize - currentAmmo;
                if (remainingAmmo - ammoToReload >= 0)
                {
                    // In this case, we can reload a full magazine..
                    currentAmmo = magazineSize;
                    remainingAmmo -= ammoToReload;
                }
                else
                {
                    // In this case, we reload the last partial magazine..
                    currentAmmo = remainingAmmo;
                    remainingAmmo = 0;
                }

                audioSrc.PlayOneShot(reloadAudio);
                UpdateAmmoText();
            }
        }
    }

    public IEnumerator ReloadingDelay()
    {
        isReloading = true;
        animator.SetBool("Reloading", true);
        yield return new WaitForSeconds(2);
        isReloading = false;
        animator.SetBool("Reloading", false);
    }

    protected void UpdateAmmoText() => txtAmmo.SetText($"{currentAmmo} / {remainingAmmo}");
}