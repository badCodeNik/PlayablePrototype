using System.Collections.Generic;
using Source.Scripts.KeysHolder;
using Source.Scripts.LibrariesSystem;
using Source.Scripts.MonoBehaviours;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.UI
{
    public class CharacterSelectionMenu : MonoBehaviour
    {
        [SerializeField] private Image previewImage; // Картинка превью
        [SerializeField] private Button selectButton; // Кнопка "Выбрать"
        [SerializeField] private Button leftArrow; // Кнопка "Листать влево"
        [SerializeField] private Button rightArrow; // Кнопка "Листать вправо"
        [SerializeField] private Sprite lockedCharacterImage;

        private List<HeroPack> _heroPacks; // Список всех возможных героев
        private int _selectedIndex = 0;

        private void OnEnable()
        {
            leftArrow.onClick.AddListener(MoveLeft);
            rightArrow.onClick.AddListener(MoveRight);
            selectButton.onClick.AddListener(SelectCharacter);
        }

        private void OnDisable()
        {
            leftArrow.onClick.RemoveListener(MoveLeft);
            rightArrow.onClick.RemoveListener(MoveRight);
            selectButton.onClick.RemoveListener(SelectCharacter);
        }

        private void Awake()
        {
            _heroPacks = Libraries.HeroPrefabLibrary.GetAllPacks();
            Debug.Log(_heroPacks.Count);
            UpdateCharacterPreview();

            DataManager.SaveCatsBought(HeroKeys.Butler);
        }

        private void UpdateCharacterPreview()
        {
            if (DataManager.LoadCatsBought(_heroPacks[_selectedIndex].ID) == 1)
            {
                previewImage.sprite = _heroPacks[_selectedIndex].Icon;
            }
            else
            {
                previewImage.sprite = lockedCharacterImage;
            }
        }

        private void MoveLeft()
        {
            if (_selectedIndex > 0)
            {
                _selectedIndex--;
                UpdateCharacterPreview();
            }
        }

        private void MoveRight()
        {
            if (_selectedIndex < _heroPacks.Count - 1)
            {
                _selectedIndex++;
                UpdateCharacterPreview();
            }

            Debug.Log(_selectedIndex);
        }

        private void SelectCharacter()
        {
            if (DataManager.LoadCatsBought(_heroPacks[_selectedIndex].ID) == 1)
            {
                DataManager.SaveSelectedCat(_heroPacks[_selectedIndex].ID);
                Debug.Log($"Selected cat is : {_heroPacks[_selectedIndex].ID}");
            }
            else
            {
                Debug.Log($"The cat : {_heroPacks[_selectedIndex]} is still closed. Save up some money to buy it :)");
            }
        }
    }
}