using UnityEngine;

namespace Source.Scripts.MonoBehaviours
{
    public class AuraController : MonoBehaviour
    {
        [SerializeField] private GameObject auraPrefab;
        [SerializeField] private Transform playerTransform;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private int auraCount = 3; // Количество аур
        [SerializeField] private float auraRadius = 2f; // Радиус расположения аур
        
        private GameObject[] _auraObjects;
        private void Start()
        {
            _auraObjects = new GameObject[auraCount];
            
            for (int i = 0; i < auraCount; i++)
            {
                // Создаем новую ауру из префаба
                _auraObjects[i] = Instantiate(auraPrefab, transform.position, Quaternion.identity);

                // Позиционируем ауру вокруг персонажа
                float angle = i * (360f / auraCount); // Равномерно распределяем ауры вокруг круга
                Vector3 offset = Quaternion.Euler(0f, 0f, angle) * Vector3.right * auraRadius; // Вычисляем смещение для каждой ауры
                _auraObjects[i].transform.position = playerTransform.position + offset;
            }
        }
        
        private void Update()
        {
            // Поворачиваем каждую ауру вокруг персонажа
            for (int i = 0; i < auraCount; i++)
            {
                _auraObjects[i].transform.RotateAround(playerTransform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
            }
        }


    }
}
