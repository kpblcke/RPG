using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Stats
{
    public class LevelDisplay : MonoBehaviour
    {
        BaseStats baseStats;
        private TextMeshProUGUI _textMeshPro;

        private void Awake()
        {
            baseStats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
        }
        
        public void Start()
        {
            _textMeshPro = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            _textMeshPro.text = String.Format("{0:0}", baseStats.GetLevel());
        }
    }
}