using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float health = 100f;
        [SerializeField] private bool isAlive = true;

        public bool IsAlive()
        {
            return isAlive;
        }
        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0);
            if (health == 0 && isAlive)
            {
                isAlive = false;
                GetComponent<Animator>().SetTrigger("die");
            }
            print(health);
        } 
    }
}
