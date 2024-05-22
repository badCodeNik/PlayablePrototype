using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.UI
{
    public class StoryViewer : MonoBehaviour
    {
        [SerializeField] private GameObject storyPanel;
        [SerializeField] private Image storyImage;
        [SerializeField] private Sprite[] storySprites;

        private int _currentImageIndex = 0;

        private void Start()
        {
            storyPanel.SetActive(false);
        }

        // Вызывается при нажатии на кнопку "История"
        public void OnHistoryButtonPressed()
        {
            storyPanel.SetActive(true);
            _currentImageIndex = 0;
            storyImage.sprite = storySprites[_currentImageIndex];
        }


        public void OnScreenTapped()
        {
            _currentImageIndex++;

            if (_currentImageIndex >= storySprites.Length)
            {
                storyPanel.SetActive(false);
            }
            else
            {
                storyImage.sprite = storySprites[_currentImageIndex];
            }
        }
    }
}