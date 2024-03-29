using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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
                 DesableUI(_isEnabled);
                 break;
             case false:
                 _isEnabled = true;
                 EnableUi(_isEnabled);
                 DesableUI(_isEnabled);
                 break;
         }
    }

    private void EnableUi(bool isEnabled)
    {
        uiToEnable.SetActive(isEnabled);
    }

    private void DesableUI(bool isEnabled)
    {
        var isDisabled = !isEnabled;
        if(uiToDisable != null) uiToDisable.SetActive(isDisabled);
    }
}
