using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Stats
{
    public class ExpirienceDisplay : MonoBehaviour
    {
        Experience experience;
        private TextMeshProUGUI _textMeshPro;

        private void Awake()
        {
            experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
        }

        public void Start()
        {
            _textMeshPro = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            _textMeshPro.text = String.Format("{0:0}", experience.getExperiencePoints());
        }
    }
}