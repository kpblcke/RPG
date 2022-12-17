using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 9;
    [SerializeField] bool isHoming = true;
    [SerializeField] GameObject hitEffect = null;
    private Health target = null;
    private float damage = 0;

    private void Start() 
    {
        transform.LookAt(GetAimLocation());
    }
    
    void Update()
    {
        if (target == null) return;
        if (isHoming)
        {
            if (target.IsAlive())
            {
                transform.LookAt(GetAimLocation());
            }
        }
        transform.Translate(Vector3.forward * speed * Time.deltaTime);      
    }
    
    public void SetTarget(Health target, float damage)
    {
        this.target = target;
        this.damage = damage;
    }

    private Vector3 GetAimLocation()
    {
        CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
        if (targetCapsule == null)
        {
            return target.transform.position;
        }
        return target.transform.position + Vector3.up * targetCapsule.height / 2;
    }
    
    private void OnTriggerEnter(Collider other) 
    {
        if(other.GetComponent<Health>() != target) return;
        if(!target.IsAlive()) return;
        target.TakeDamage(damage);
        
        if(hitEffect != null)
        {
            Instantiate(hitEffect, GetAimLocation(), transform.rotation);
        }
        
        Destroy(gameObject); 
    }

}