using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChestBase : MonoBehaviour
{
    public KeyCode keyCode = KeyCode.Z;
    public Animator animator;
    public string triggerOpen = "Open";

    [Header("Notification")]
    public GameObject notification;
    public float tweenDuration = .2f;
    public Ease tweenEase = Ease.OutBack;

    [Space]
    public ChestItemBase chestItem;


    private float startScale;

    private bool _chestOpened = false;

    // Start is called before the first frame update
    void Start()
    {
        startScale = notification.transform.localScale.x;
        HideNotification();
    }


    [NaughtyAttributes.Button]
    private void OpenChest()
    {
        if(_chestOpened) return;
        animator.SetTrigger(triggerOpen);
        _chestOpened = true;
        HideNotification();
        chestItem.ShowItem();
        Invoke(nameof(ShowItem), 1f);
    }

    private void ShowItem()
    {
        chestItem.ShowItem();
        Invoke(nameof(CollectItem), 1f);
    }

    private void CollectItem()
    {
        chestItem.Collect();
    }


    public void OnTriggerEnter(Collider other)
    {
        Player p = other.transform.GetComponent<Player>();
        if (p != null)
        {
            ShowNotification();

        }
    }

    public void OnTriggerExit(Collider other)
    {
        Player p = other.transform.GetComponent<Player>();
        if (p != null)
        {
            HideNotification();

        }
    }

    [NaughtyAttributes.Button]
    private void ShowNotification()
    {
        notification.SetActive(true);
        notification.transform.localScale = Vector3.zero;
        notification.transform.DOScale(startScale, tweenDuration);
    }

    [NaughtyAttributes.Button]
    private void HideNotification()
    {
        notification.SetActive(false); 
    }

    private void Update()
    {
        if(Input.GetKeyDown(keyCode) && notification.activeSelf)
        {
            OpenChest();
        }
    }


}
