using Source.Scripts.KeysHolder;
using Source.SignalSystem;

namespace Source.Scripts.PerkScripts
{
    using UnityEngine;
    using UnityEngine.UI;

    public class PerkButton : MonoSignalListener<OnPerksGenerated>
    {
        private PerkKeys _perkKey;

        private Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(SelectPerk);
        }

        private void SelectPerk()
        {
            signal.RegistryRaise(new OnPerkChosenSignal()
            {
                ChosenPerkID = _perkKey
            });
        }

        protected override void OnSignal(OnPerksGenerated data)
        {
            Debug.Log("Kek");
            PerkButton[] perkButtons = GetAllPerkButtons();
            Debug.Log(perkButtons.Length);
            if (perkButtons.Length != data.PerksPacks.Count)
            {
                Debug.LogError("Number of buttons does not match number of perks");
                return;
            }

            for (int i = 0; i < perkButtons.Length; i++)
            {
                perkButtons[i]._perkKey = data.PerksPacks[i].ID;
            }
        }

        private static PerkButton[] GetAllPerkButtons()
        {
            return FindObjectsOfType<PerkButton>();
        }
    }
}