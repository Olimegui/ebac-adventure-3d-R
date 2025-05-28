using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable 
{

    public void Damage(float damage)
    {
        throw new System.NotImplementedException();
    }
    

    public void Damage(float damage, Vector3 dir)
    {
        throw new System.NotImplementedException();
    }
}
