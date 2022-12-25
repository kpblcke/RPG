using System;
using RPG.Attributes;
using RPG.Combat;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter fighter;
        private TextMeshProUGUI _textMeshPro;

        private void Awake()
        {
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
        }

        public void Start()
        {
            _textMeshPro = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            if (fighter == null)
            {
                _textMeshPro.text = "N/A";
                return;
            }
            Health health = fighter.GetTarget();
            if (health == null)
            {
                _textMeshPro.text = "Dead";
            } else {
                _textMeshPro.text = String.Format("{0:0}/{1:0}", health.GetHealthPoints(), health.GetMaxHealthPoints());
            }
        }
    }
}