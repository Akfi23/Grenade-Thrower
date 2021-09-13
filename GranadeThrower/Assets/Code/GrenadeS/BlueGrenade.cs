using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueGrenade : Grenade
{
    //[SerializeField] private int _damage;
    
    private void Update()
    {
        if (isThrowed == true)
        {
            countdown -= Time.deltaTime;

            if (countdown <= 0)
                Explode(damage);
        }
    }    
}
