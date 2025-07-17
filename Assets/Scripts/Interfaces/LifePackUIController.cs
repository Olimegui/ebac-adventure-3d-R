using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePackUIController : MonoBehaviour
{
    [Header("Referência da UI do LifePack")]
    [SerializeField] private GameObject lifePackUI;

    [SerializeField] private HealthBase healthBase; // referenciado no inspector

    [SerializeField] private float healAmount = 5f; // quantidade de cura

    private bool hasLifePack = false;

    private void Start()
    {
        if (lifePackUI != null)
            lifePackUI.SetActive(false); // Começa desativado
    }

    private void Update()
    {
        if (hasLifePack && Input.GetKeyDown(KeyCode.L))
        {
            UseLifePack();
        }
    }

    public void CollectLifePack()
    {
        hasLifePack = true;

        if (lifePackUI != null)
            lifePackUI.SetActive(true);
    }

    private void UseLifePack()
    {
        if (healthBase != null)
            healthBase.Damage(-healAmount); // cura aplicando dano negativo

        hasLifePack = false;

        // Aqui você pode adicionar o código que aplica cura ao jogador
        // Exemplo fictício: playerHealth.Heal(10);

        hasLifePack = false;

        if (lifePackUI != null)
            lifePackUI.SetActive(false);
    }
}
