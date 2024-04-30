using Source.SignalSystem;
using UnityEngine;

namespace Source.Scripts.MonoBehaviours
{
    public class AuraController : MonoSignalListener<OnBurningAuraChosen, OnFreezingAuraChosen>
    {
        [SerializeField] private GameObject fireAuraPrefab;
        [SerializeField] private GameObject freezingAuraPrefab;
        [SerializeField] private Transform playerTransform;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private int auraCount = 3; 
        [SerializeField] private float auraRadius = 2f; 
        [SerializeField] private float maxDistance;

        private GameObject[] _auraObjects;
        private bool _isAuraCreated;

        private void Start()
        {
            _auraObjects = new GameObject[auraCount];
        }

        private void Update()
        {
            if (_isAuraCreated)
            {
                // Поворачиваем каждую ауру вокруг персонажа
                for (int i = 0; i < auraCount; i++)
                {
                    _auraObjects[i].transform.RotateAround(playerTransform.position, Vector3.forward,
                        rotationSpeed * Time.deltaTime);
                }
            }
        }


        protected override void OnSignal(OnBurningAuraChosen data)
        {
            for (int i = 0; i < auraCount; i++)
            {
                // Создаем новую ауру из префаба
                _auraObjects[i] = Instantiate(fireAuraPrefab, transform.position, Quaternion.identity);
                
                float angle = i * (360f / auraCount); // Равномерно распределяем ауры вокруг круга
                Vector3 offset =
                    Quaternion.Euler(0f, 0f, angle) * Vector3.right * maxDistance; // Вычисляем смещение для каждой ауры
                offset = Vector3.ClampMagnitude(offset, maxDistance);
                _auraObjects[i].transform.position = playerTransform.position + offset;
            }

            _isAuraCreated = true;
        }

        protected override void OnSignal(OnFreezingAuraChosen data)
        {
            for (int i = 0; i < auraCount; i++)
            {
                // Создаем новую ауру из префаба
                _auraObjects[i] = Instantiate(freezingAuraPrefab, transform.position, Quaternion.identity);
                
                float angle = i * (360f / auraCount); // Равномерно распределяем ауры вокруг круга
                Vector3 offset =
                    Quaternion.Euler(0f, 0f, angle) * Vector3.right * maxDistance; // Вычисляем смещение для каждой ауры
                offset = Vector3.ClampMagnitude(offset, maxDistance);
                _auraObjects[i].transform.position = playerTransform.position + offset;
            }
            _isAuraCreated = true;
        }
    }
}