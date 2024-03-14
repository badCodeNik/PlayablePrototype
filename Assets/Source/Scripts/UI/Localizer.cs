using System;
using Source.Scripts.Data;
using Source.Scripts.EventSystem;
using Source.Scripts.Extensions;
using Source.Scripts.KeysHolder;
using Source.Scripts.LibrariesSystem;
using TMPro;
using UnityEngine;

namespace Source.Scripts.UI
{
    public class Localizer : MonoBehaviour
    {
        [SerializeField] private WordKeys wordID;
        [SerializeField] private TMP_Text tmpText;
        [SerializeField] private LanguageKeyEventChannel languageKeyEventChannel;
        [SerializeField] private GameConfiguration gameConfiguration;
        [SerializeField] private LanguageLibrary languageLibrary;

        public void Start()
        {
            tmpText.text = Libraries.LanguageLibrary.GetByID(gameConfiguration.languageID).GetValueByWordID(wordID);
        }

        private void OnEnable()
        {
            languageKeyEventChannel.OnEventRaised.AddListener(OnLanguageChange);
        }

        private void OnDisable()
        {
            languageKeyEventChannel.OnEventRaised.RemoveListener(OnLanguageChange);
        }

        private void OnLanguageChange(LanguageKeys languageID)
        {
            tmpText.text = languageLibrary.GetByID(languageID).GetValueByWordID(wordID);
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