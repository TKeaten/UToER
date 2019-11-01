using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    
    /* Raycast shooting
     * public int damage = 25;
     * public GameObject impactEffect;
     * public LineRenderer lineRenderer;
     */

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
            /* Raycast shooting
             * StartCoroutine(Shoot());
             */
        }
    }

    void Shoot ()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
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
