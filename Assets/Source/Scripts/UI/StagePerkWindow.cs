using System.Collections.Generic;
using Source.Scripts.LibrariesSystem;
using Source.SignalSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.UI
{
    public class StagePerkWindow : MonoSignalListener<OnPerksGenerated>
    {
        [SerializeField] private GameObject panel;
        [SerializeField] private Animator animator;
        [SerializeField] private Image[] perkImages;
        [SerializeField] private TextMeshProUGUI[] perkDescriptions;

        private List<PerksPack> _perksPacks = new List<PerksPack>();


        protected override void OnSignal(OnPerksGenerated data)
        {
            for (int i = 0; i < data.PerksPacks.Count; i++)
            {
                perkImages[i].sprite = data.PerksPacks[i].Icon;
                perkDescriptions[i].text = data.PerksPacks[i].Description;
            }
        }
    }
}