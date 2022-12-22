using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Attributes
{
    public class HealthDisplay : MonoBehaviour
    {
        Health health;
        private TextMeshProUGUI _textMeshPro;

        private void Awake()
        {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
        }

        public void Start()
        {
            _textMeshPro = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            _textMeshPro.text = String.Format("{0:0}%", health.GetPercentage());
        }
    }
}