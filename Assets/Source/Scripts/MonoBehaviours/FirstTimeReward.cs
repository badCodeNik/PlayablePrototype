using UnityEngine;

namespace Source.Scripts.MonoBehaviours
{
    public class FirstTimeReward : MonoBehaviour
    {
        // Количество монет для начисления
        private const int FirstTimeCoins = 100;

        // Ключ для проверки первого запуска
        private const string FirstLaunchKey = "IsFirstLaunch";

        private void Start()
        {
            // Проверяем, была ли игра уже запущена
            if (IsFirstLaunch())
            {
                GrantFirstTimeCoins();
                // Устанавливаем флаг, что игра была запущена
                PlayerPrefs.SetInt(FirstLaunchKey, 1);
                PlayerPrefs.Save();
            }
        }

        private bool IsFirstLaunch()
        {
            return PlayerPrefs.GetInt(FirstLaunchKey, 0) == 0;
        }

        private void GrantFirstTimeCoins()
        {
            Debug.Log($"Начисляем {FirstTimeCoins} монет за первый вход в игру.");
            DataManager.AddCrystals(FirstTimeCoins);
        }
    }
}