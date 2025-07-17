using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Itens;

public class ActionLifePack : MonoBehaviour
{
    public static ActionLifePack Instance;

    public KeyCode keyCode = KeyCode.L;
    public GameObject hudLifePack;
    public SOInt soInt;

    private void Awake()
    {
        Instance = this;
        if(hudLifePack != null) hudLifePack.SetActive(false);
    }

    public void EnableUsage()
    {
        //Prepara referencia ao contador de LifePack
        soInt = ItemManager.Instance.GetItemByType(ItemType.LIFE_PACK).soInt;

    }


    private void Start()
    {
      soInt = ItemManager.Instance.GetItemByType(ItemType.LIFE_PACK).soInt;
    }

    private void Update()
    {

        if (Input.GetKeyDown(keyCode))
        {
            RecoverLife();
        }
    }

    public void RecoverLife()
    {
        if(soInt != null && soInt.value > 0)
        {
            ItemManager.Instance.RemoveByType(ItemType.LIFE_PACK);
            Player.Instance.healthBase.ResetLife();

            if(hudLifePack != null) hudLifePack.SetActive(false);
        }
    }
}
