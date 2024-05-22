using UnityEngine;

namespace Source.Scripts.UI
{
    public class MenuManager : MonoBehaviour
    {
        // Пример для четырех разных меню
        public GameObject menu1;
        public GameObject menu2;
        public GameObject menu3;

        private void CloseAllMenus()
        {
            menu1.SetActive(false);
            menu2.SetActive(false);
            menu3.SetActive(false);
        }

        public void OnMenuButtonClicked(GameObject menu)
        {
            bool isMenuActive = menu.activeSelf;
            CloseAllMenus();

            menu.SetActive(!isMenuActive);
        }
    }
}