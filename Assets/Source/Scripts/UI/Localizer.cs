
using Source.Scripts.Data;
using Source.Scripts.KeysHolder;
using Source.Scripts.LibrariesSystem;
using Source.SignalSystem;
using TMPro;
using UnityEngine;

namespace Source.Scripts.UI
{
    public class Localizer : MonoSignalListener<OnLanguageChangedSignal>
    {
        [SerializeField] private WordKeys wordID;
        [SerializeField] private TMP_Text tmpText;
        [SerializeField] private GameConfiguration gameConfiguration;
        [SerializeField] private LanguageLibrary languageLibrary;

        public void Start()
        {
            tmpText.text = Libraries.LanguageLibrary.GetByID(gameConfiguration.languageID).GetValueByWordID(wordID);
        }

        protected override void OnSignal(OnLanguageChangedSignal data)
        {
            tmpText.text = languageLibrary.GetByID(data.CurrentValue).GetValueByWordID(wordID);
        }
        
        private void OnValidate()
        {
            if (tmpText == null) tmpText = GetComponent<TMP_Text>();
            if (tmpText != null)
            {
                tmpText.text = languageLibrary.GetByIDIterations(LanguageKeys.En).GetValueByWordID(wordID);;
            }
        }
    }
}