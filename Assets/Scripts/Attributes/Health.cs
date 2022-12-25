using System.Collections;
using System.Collections.Generic;
using GameDevTV.Utils;
using RPG.Core;
using RPG.Saving;
using RPG.Stats;
using UnityEngine;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float regenerationPercentage = 70;
        [SerializeField] private bool isAlive = true;
        private LazyValue<float> health;

        private void Awake() {
            health = new LazyValue<float>(GetInitialHealth);
        }
        
        private float GetInitialHealth()
        {
            return GetComponent<BaseStats>().GetStat(StatType.Health);
        }

        private void Start()
        {
            health.ForceInit();
        }
        
        private void OnEnable() {
            GetComponent<BaseStats>().onLevelUp += RegenerateHealth;
        }

        private void OnDisable() {
            GetComponent<BaseStats>().onLevelUp -= RegenerateHealth;
        }
        
        public bool IsAlive()
        {
            return isAlive;
        }

        public float GetHealthPoints()
        {
            return health.value;
        }
        
        public float GetMaxHealthPoints()
        {
            return GetComponent<BaseStats>().GetStat(StatType.Health);
        }

        public float GetPercentage()
        {
            return 100 * (health.value / GetComponent<BaseStats>().GetStat(StatType.Health));
        }
        
        public void TakeDamage(GameObject instigator, float damage)
        {
            print(gameObject.name + " took damage: " + damage);
            health.value = Mathf.Max(health.value - damage, 0);
            if (health.value == 0)
            {
                Die();
                AwardExperience(instigator);
            }
        } 
        
        private void Die()
        {
            if (!isAlive) return;
            isAlive = false;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
        
        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) return;

            experience.GainExperience(GetComponent<BaseStats>().GetStat(StatType.ExperienceReward));
        }
        
        private void RegenerateHealth()
        {
            float regenHealthPoints = GetComponent<BaseStats>().GetStat(StatType.Health) * (regenerationPercentage / 100);
            health.value = Mathf.Max(health.value, regenHealthPoints);
        }
        
        public object CaptureState()
        {
            return health.value;
        }

        public void RestoreState(object state)
        {
            health.value = (float)state;

            if (health.value == 0)
            {
                Die();
            }
        }
    }
}
