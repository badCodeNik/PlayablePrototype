using DG.Tweening;
using Source.Scripts.KeysHolder;
using Source.Scripts.LibrariesSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.UI
{
    public class PerkCard : MonoBehaviour
    {
        [SerializeField] private Image perkImage; 
        [SerializeField] private TextMeshProUGUI perkDescription;
        
        public Image PerkImage => perkImage;
        private Tween _tween;

        public void SetContent(PerkKeys perkID)
        {
            perkImage.sprite = Libraries.Instance.PerksLibrary.GetByID(perkID).Icon;
            _tween = perkImage.DOFade(1, 1.5f);
            perkDescription.text = Libraries.Instance.PerksLibrary.GetByID(perkID).Description;
        }
    }
}
