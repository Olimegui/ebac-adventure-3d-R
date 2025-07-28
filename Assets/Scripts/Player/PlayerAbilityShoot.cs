using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    [Header("Armas disponíveis")]
    public List<GunBase> availableGuns; //Armas possíveis
    public Transform gunPosition;

    //public GunBase gunBase;
    private GunBase _currentGun;
    private int currentGunIndex = 0;
    public FlashColor _flashColor;


    protected override void Init()
    {
        base.Init();

        CreateGun();

        inputs.Gameplay.Shoot.performed += cts => StartShoot();
        inputs.Gameplay.Shoot.canceled += cts => CancelShoot();
    }

    private void Update()
    {
        //Troca de arma usando 1 e 2
        if (Input.GetKeyDown(KeyCode.Alpha1)) ChangeGun(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) ChangeGun(1);
    }

    private void CreateGun()
    {
        //Remove arma anterior
        if (_currentGun != null)
        {
            Destroy(_currentGun.gameObject);
        }

        //Instancia nova arma
        _currentGun = Instantiate(availableGuns[currentGunIndex], gunPosition);
        _currentGun.transform.localPosition = Vector3.zero;
        _currentGun.transform.localRotation = Quaternion.identity;

        //Define dono para ignorar colisão
        _currentGun.owner = Player.Instance.gameObject;

        var projectileCollider = _currentGun.prefabProjectile.GetComponent<Collider>();
        if (projectileCollider != null)
        {
            Player.Instance.colliders.ForEach(col =>
            {
                if (col != null)
                {
                    Physics.IgnoreCollision(projectileCollider, col);
                }
            });
        }
    }

    private void ChangeGun(int index)
    {
        if(index >= 0 && index < availableGuns.Count)
        {
            currentGunIndex = index;
            CreateGun();
        }
    }

    private void StartShoot()
    {
        if (_currentGun != null)
        {
          _currentGun.StartShoot();
            _flashColor?.Flash();
          Debug.Log("Start Shoot");

        }
    }

    private void CancelShoot()
    {
        if (_currentGun != null)
        {
          _currentGun.StopShoot();
          Debug.Log("Cancel Shoot");

        }
    }
}

