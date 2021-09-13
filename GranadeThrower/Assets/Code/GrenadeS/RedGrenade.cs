using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedGrenade : Grenade
{    
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
