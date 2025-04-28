using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityBase : MonoBehaviour
{
    protected Player player;

    protected Inputs inputs;

    private void Onvalidade()
    {
        if (player == null) player = GetComponent<Player>();
    }

    private void Start()
    {
        inputs = new Inputs();
        inputs.Enable();


        Init();
        Onvalidade();
        RegisterListeners();
    }

    private void OnEnable()
    {
        if(inputs != null)
            inputs.Enable();
    }

    private void OnDisable()
    {
        inputs.Disable();
    }

    private void OnDestroy()
    {
        RemoveListerners();
    }

    protected virtual void Init() { }

    protected virtual void RegisterListeners() { }

    protected virtual void RemoveListerners() { }

}
