using UnityEngine;

namespace Source.Scripts.UI
{
    public class ButtonEnableUI : MonoBehaviour
    {
        [SerializeField] private GameObject uiToEnable;
        [SerializeField] private GameObject uiToDisable;
        private bool _isEnabled;

        public void EnableUi()
        {
            switch (_isEnabled)
            {
                case true:
                    _isEnabled = false;
                    EnableUi(_isEnabled);
                    DisableUI(_isEnabled);
                    break;
                case false:
                    _isEnabled = true;
                    EnableUi(_isEnabled);
                    DisableUI(_isEnabled);
                    break;
            }
        }

        private void EnableUi(bool isEnabled)
        {
            uiToEnable.SetActive(isEnabled);
        }

        private void DisableUI(bool isEnabled)
        {
            var isDisabled = !isEnabled;
            if (uiToDisable != null) uiToDisable.SetActive(isDisabled);
        }
    }
}