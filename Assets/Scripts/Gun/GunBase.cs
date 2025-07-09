using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public ProjectileBase prefabProjectile;

    public Transform positionToShoot;
    public float timeBetweenShoot = .3f;
    public float speed = 50f;

    private Coroutine _currentCoroutine;


    protected virtual IEnumerator ShootCoroutine()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweenShoot);
        }
    }

    public virtual void Shoot()
    {
        var projectile = Instantiate(prefabProjectile);
        projectile.transform.position = positionToShoot.position;
        projectile.transform.rotation = positionToShoot.rotation;
        projectile.speed = speed;

        //Ignora colisão com o jogador
        var projectileCollider = projectile.GetComponentInChildren<Collider>();
        if (projectileCollider != null && Player.Instance != null)
        {
            foreach (var col in Player.Instance.colliders)
            {
                if(col != null)
                {
                    // Temporariamente desativa o Collider para evitar colisão ao nascer
                    projectileCollider.enabled = false;

                    // Reativa o Collider após pequeno delay (evita colisão inicial com o player)
                    projectile.StartCoroutine(EnableColliderAfterDelay(projectileCollider, 0.1f));

                    Physics.IgnoreCollision(projectileCollider, col);
                }
            }
        }

        ShakeCamera.Instance.Shake();
    }

    private IEnumerator EnableColliderAfterDelay(Collider collider, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (collider != null) collider.enabled = true;
    }



    public void StartShoot()
    {
        StopShoot();
        _currentCoroutine = StartCoroutine(ShootCoroutine());
    }

    public void StopShoot()
    {
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);
    }
}
