using System;
using Source.Scripts.LibrariesSystem;
using UnityEngine;

namespace Source.Scripts.MonoBehaviours
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] enemies;

        private void Start()
        {
            
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) foreach (var enemy in enemies) Instantiate(enemy, transform.position, Quaternion.identity);
                
            

        }
    }
}
