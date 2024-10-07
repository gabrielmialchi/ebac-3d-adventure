using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShootLimit : GunBase
{
    public List<UIFillUpdater> uIGunUpdaters;

    public int maxBullets = 5;
    public float rechargeTime = 1f;

    private int _currentShoot;
    private bool _recharging = false;

    private void Awake()
    {
        GetAllUIs();
    }

    protected override IEnumerator ShootCoroutine()
    {
        if (_recharging) yield break;

        while (true)
        {
            if (_currentShoot < maxBullets)
            {
                Shoot();
                _currentShoot++;
                CheckRecharge();
                UpdateUI();
                yield return new WaitForSeconds(attackSpeed);
            }
        }
    }
    private void CheckRecharge()
    {
        if (_currentShoot >= maxBullets)
        {
            StopShoot();
            StartRecharge();
        }
    }

    private void StartRecharge()
    {
        _recharging = true;
        StartCoroutine(RechargeCoroutine());
    }

    IEnumerator RechargeCoroutine()
    {
        float time = 0f;
        while (time < rechargeTime)
        {
            time += Time.deltaTime;
            uIGunUpdaters.ForEach(i => i.UpdateValue(time/rechargeTime));
            yield return new WaitForEndOfFrame();
        }
        _currentShoot = 0;
        _recharging = false;
    }

    private void UpdateUI()
    {
        uIGunUpdaters.ForEach(i => i.UpdateValue(maxBullets, _currentShoot));
    }
    
    private void GetAllUIs()
    {
        uIGunUpdaters = GameObject.FindObjectsOfType<UIFillUpdater>().ToList();
    }
}
