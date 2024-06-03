using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    public KeyCode gun1Key = KeyCode.Alpha1;
    public KeyCode gun2Key = KeyCode.Alpha2;

    public List<GunBase> listOfGuns;

    public GunBase gunBase;
    public Transform gunPosition;

    private GunBase _currentGun;

    protected override void Init()
    {
        base.Init();

        CreateGun();

        inputs.Gameplay.Shoot.performed += ctx => StartShoot();
        inputs.Gameplay.Shoot.canceled += ctx => CancelShoot();
    }

    private void Update()
    {
        ChangeGun();
    }

    private void CreateGun()
    {
        _currentGun = Instantiate(gunBase, gunPosition);

        _currentGun.transform.localPosition = _currentGun.transform.localEulerAngles = Vector3.zero;
    }

    private void ChangeGun()
    {
        if (Input.GetKeyDown(gun1Key))
        {
            gunBase = listOfGuns[0];
        }

        if (Input.GetKeyDown(gun2Key))
        {
            gunBase = listOfGuns[1];
        }
    }

    private void StartShoot()
    {
        _currentGun.StartShoot();
        //Debug.Log("Start Shoot");
    }

    private void CancelShoot()
    {
        //Debug.Log("Cancel Shoot");
        _currentGun.StopShoot();
    }
}