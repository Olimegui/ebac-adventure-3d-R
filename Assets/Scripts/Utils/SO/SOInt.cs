using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/SOInt")]
public class SOInt : ScriptableObject
{
    public int value;

    public void Add(int amount)
    {
        value += amount;
    }

    public void Subtract(int amount)
    {
        value = Mathf.Max(0, value - amount); // impede valor negativo
    }

    public void ResetValue()
    {
        value = 0;
    }
}