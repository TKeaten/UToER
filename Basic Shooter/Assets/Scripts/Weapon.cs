using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public CharacterController2D controller;
    public Transform firePoint;
    public Transform firePointUp;
    public GameObject bulletPrefab;
    public GameObject bulletPrefabUp;
    public Animator anim;
    public float shotDelay;

    /* Raycast shooting
     * public int damage = 25;
     * public GameObject impactEffect;
     * public LineRenderer lineRenderer;
     */

    // Update is called once per frame
    void Update()
    {
        if (shotDelay <= 0 && PauseMenu.isPaused == false)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                /*if (Input.GetKey("w"))
                {
                    ShootUp();
                } else
                {
                    Shoot();
                }*/

                Shoot();

                /* Raycast shooting
                 * StartCoroutine(Shoot());
                 */
            }
        }
    }

    private void FixedUpdate()
    {
        shotDelay -= Time.deltaTime;
    }

    void Shoot ()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        anim.SetBool("Shooting", true);
        shotDelay = 0.4f;
    }

    /*
     * This is a super ugly way to go about changing the nature of the shot.  Will need to change.
     * void ShootUp()   
    {
        Instantiate(bulletPrefabUp, firePointUp.position, firePointUp.rotation);
        anim.SetBool("ShootingUp", true);
    }
    */
    void AnimationEnded()
    {
        anim.SetBool("Shooting", false);
        anim.SetBool("ShootingUp", false);
    }

    /* Raycast shooting
     * IEnumerator Shoot()
     * {
     * RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right);
     * if (hitInfo)
     * {
     * EnemyCrab enemy = hitInfo.transform.GetComponent<EnemyCrab>();
     * Debug.Log(hitInfo.transform.name);
     * if (enemy != null)
     * {
     * enemy.TakeDamage(damage);
     * }
     * Instantiate(impactEffect, hitInfo.point, Quaternion.identity);
     * lineRenderer.SetPosition(0, firePoint.position);
     * lineRenderer.SetPosition(1, hitinfo.point);
     * } else 
     * {
     * lineRenderer.SetPosition(0, firePoint.position);
     * lineRenderer.SetPosition(1, firePoint.position + firePoint.right * 100);
     * }
     * lineRenderer.enabled = true;
     * yield return null;
     * lineRenderer.enabled = false;
     *}
     */
}
