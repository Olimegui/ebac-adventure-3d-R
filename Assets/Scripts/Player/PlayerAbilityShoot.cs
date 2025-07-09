using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase
{

    public GunBase gunBase;
    public Transform gunPosition;

    private GunBase _currentGun;

    protected override void Init()
    {
        base.Init();

        CreateGun();

        inputs.Gameplay.Shoot.performed += cts => StartShoot();
        inputs.Gameplay.Shoot.canceled += cts => CancelShoot();
    }

    private void CreateGun()
    {
        _currentGun = Instantiate(gunBase, gunPosition);

        var projectileCollider = _currentGun.prefabProjectile.GetComponent<Collider>();
        foreach (var col in Player.Instance.colliders)
        {
            Physics.IgnoreCollision(projectileCollider, col);
        }

        //_currentGun.transform.position = _currentGun.transform.localEulerAngles = Vector3.zero;
        _currentGun.transform.localPosition = Vector3.zero;
        _currentGun.transform.localRotation = Quaternion.identity;
    }

    private void StartShoot()
    {
        if (_currentGun != null) _currentGun.StartShoot();
        _currentGun.StartShoot();
        Debug.Log("Start Shoot");
    }

    private void CancelShoot()
    {
        Debug.Log("Cancel Shoot");
        _currentGun.StopShoot();
    }
}

