using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public ProjectileBase prefabProjectile;
    public Transform positionToShoot;
    public List<UIFillUpdater> uIGunUpdaters;

    public float attackSpeed = .3f;
    public float speed = 50f;
    public int maxAmmo;

    private Coroutine _currentCoroutine;

    private bool isShooting = false;
    private int _currentAmmo;

    protected virtual IEnumerator ShootCoroutine()
    {
        while (isShooting)
        {
            Shoot();
            yield return new WaitForSeconds(attackSpeed);
        }
    }

    public virtual void Shoot()
    {
        var projectile = Instantiate(prefabProjectile);
        projectile.transform.position = positionToShoot.position;
        projectile.transform.rotation = positionToShoot.rotation;
        projectile.projectileSpeed = speed;
    }

    public void StartShoot()
    {
        //StopShoot();
        if (!isShooting)
        {
            isShooting = true;
            _currentCoroutine = StartCoroutine(ShootCoroutine());
        }
    }

    public void StopShoot()
    {
        isShooting = false;
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }
    }

    public void UpdateUI()
    {
        if (uIGunUpdaters != null)
        {
            uIGunUpdaters.ForEach(i => i.UpdateValue((float)_currentAmmo / maxAmmo));
        }
    }

    public void GetMaxBullets()
    {

    }
}
