using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyShoot : EnemyBase
    {
        public GunBase enemyGunBase;
        public Transform playerTransform;
        public float detectionRange = 15f;
        public float rotationSpeed = 5f; // velocidade da rotação
        private bool isShooting = false;

        protected override void Init()
        {
            base.Init();

            if (enemyGunBase == null)
            {
                Debug.LogWarning("EnemyShoot: GunBase não foi atribuído!");
                return;
            }

            // Define o dono da arma como o próprio inimigo
            enemyGunBase.owner = this.gameObject;

            // Ativa a arma se estiver desativada
            if (!enemyGunBase.gameObject.activeInHierarchy)
            {
                enemyGunBase.gameObject.SetActive(true);
            }


        }

        void Update()
        {
            if (playerTransform == null) return;

            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

            if (distanceToPlayer < detectionRange)
            {
                // Rotaciona o corpo do inimigo suavemente em direção ao player
                Vector3 directionToPlayer = playerTransform.position - transform.position;
                directionToPlayer.y = 0f; // evita inclinação vertical

                Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

                // Disparo
                if (!isShooting)
                {
                    enemyGunBase.StartShoot();
                    isShooting = true;
                }
            }
            else
            {
                if (isShooting)
                {
                    enemyGunBase.StopShoot();
                    isShooting = false;
                }
            }

        }
    }
}