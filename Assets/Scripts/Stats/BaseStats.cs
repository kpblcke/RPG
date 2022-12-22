using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;
        
        private void Update() 
        {
            if (gameObject.tag == "Player")
            {
                print(GetLevel());
            }
        }
        
        public float GetStat(StatType stat)
        {
            return progression.GetStat(stat, characterClass, GetLevel());
        }
        
        public int GetLevel()
        {
            Experience experience = GetComponent<Experience>();
            if (experience == null) return startingLevel;

            float currentXP = experience.getExperiencePoints();
            int penultimateLevel = progression.GetLevels(StatType.ExperienceToLevelUp, characterClass);
            for (int level = 1; level <= penultimateLevel; level++)
            {
                float XPToLevelUp = progression.GetStat(StatType.ExperienceToLevelUp, characterClass, level);
                if (XPToLevelUp > currentXP)
                {
                    return level;
                }
            }

            return penultimateLevel + 1;
        }
    }
}