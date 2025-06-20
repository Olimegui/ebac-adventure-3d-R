using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityBase : MonoBehaviour
{
    protected Player player;

    protected Inputs inputs;

    private void OnValidate()
    {
        if (player == null) player = GetComponent<Player>();
    }

    private void Start()
    {
        inputs.Enable();


        Init();
        OnValidate();
        RegisterListeners();
    }

    private void Awake()
    {
        if (inputs == null)
           inputs = new Inputs();
    }

    private void OnEnable()
    {
        if(inputs != null)
            inputs.Enable();
    }

    private void OnDisable()
    {
        if (inputs != null)
        {
            inputs.Gameplay.Disable();

        }

        Init();
        OnValidate();
        RegisterListeners();
    }

    private void OnDestroy()
    {
        if (inputs != null)
        {
            inputs.Dispose();
            inputs = null;
        }

        RemoveListeners();
    }

    protected virtual void Init() { }

    protected virtual void RegisterListeners() { }

    protected virtual void RemoveListeners() { }

}
