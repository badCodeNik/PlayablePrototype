using UnityEngine;

namespace Source.Scripts.UI
{
    public class MenuManager : MonoBehaviour
    {
        // Пример для четырех разных меню
        public GameObject menu1;
        public GameObject menu2;
        public GameObject menu3;

        public void CloseAllMenus()
        {
            // Закрываем все меню
            menu1.SetActive(false);
            menu2.SetActive(false);
            menu3.SetActive(false);
        }

        public void OnMenuButtonClicked(GameObject menu)
        {
            bool isMenuActive = menu.activeSelf;
            CloseAllMenus(); // Сначала закрываем все меню

            // Затем переключаем состояние нажатого меню
            // (если оно было открыто, теперь закрыть - если было закрыто, теперь открыть)
            menu.SetActive(!isMenuActive);
        }
    }
}
