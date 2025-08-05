using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Itens
{
    public class ItemCollactableBase : MonoBehaviour
    {
        
        public bool _collected = false;

        public SFXType sfxType;
        public ItemType itemType;


        public string compareTag = "Player";
        public ParticleSystem particleSystem;
        public float timeToHide = 3;
        public GameObject graphicItem;

        public Collider collider;

        [Header("Sounds")]
        public AudioSource audioSource;

        private void Awake()
        {
            //if (particleSystem != null) particleSystem.transform.SetParent(null);
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (_collected) return;

            if (collision.transform.CompareTag(compareTag))
            {
                _collected = true;
                Collect();
            }
        }

        private void PlaySFX()
        {
            SFXPool.Instance.Play(sfxType);
        }

        protected virtual void OnCollect()
        {
            if (collider != null) collider.enabled = false;
            if (graphicItem != null) graphicItem.SetActive(false);

            ItemManager.Instance.AddByType(itemType);
        }

        protected virtual void Collect()
        {
            PlaySFX();
            if (collider != null) collider.enabled = false;
            if (graphicItem != null) graphicItem.SetActive(false);
            Invoke("HideObject", timeToHide);
            OnCollect();
        }

        private void HideObject()
        {
            gameObject.SetActive(false);
        }

    }

    public class ItemLifePack : ItemCollactableBase
    {
        public GameObject hudLifePack;

        protected override void OnCollect()
        {
            base.OnCollect(); // toca partículas, som e atualiza ItemManager

            if (hudLifePack != null)
                hudLifePack.SetActive(true); // ativa HUD ao coletar

            if (ActionLifePack.Instance != null)
                ActionLifePack.Instance.EnableUsage(); // permite uso com L
        }
    }

}