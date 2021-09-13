using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public abstract  class Grenade : MonoBehaviour
{
    [SerializeField] protected int damage;
    [SerializeField] protected float timer;
    [SerializeField] protected float countdown;
    [SerializeField] protected float radius;
    [SerializeField] protected ParticleSystem explossionEffect;
    public Vector3 GrenadePos { get; private set; }

    public  bool isThrowed;
    private void Start()
    {
        countdown = timer;
        GrenadePos = gameObject.transform.position;
    }
    
    protected void Explode(int dmg) 
    {
        ParticleSystem newEffect= Instantiate(explossionEffect, transform.position, transform.rotation);
        Destroy(newEffect.gameObject, 1);        

        Collider[] colliders= Physics.OverlapSphere(transform.position,radius);

        foreach (var objects in colliders)
        {
            if(objects.gameObject.TryGetComponent<Enemy>(out Enemy enemy)) 
            {
                enemy.TakeDamage(dmg);
            }
        }

        Destroy(gameObject);
    }    
}
