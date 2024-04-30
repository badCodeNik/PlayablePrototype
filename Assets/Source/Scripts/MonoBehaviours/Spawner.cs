using System.Collections;
using Source.Scripts.KeysHolder;
using Source.Scripts.LibrariesSystem;
using UnityEngine;

namespace Source.Scripts.MonoBehaviours
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private EnemyKeys enemyId;
        private const float SpawnDelay = 1f;

        private void Start()
        {
            StartCoroutine(SpawnEnemiesAfterDelay());
            // SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            //spriteRenderer.sprite = enemy.Icon;
        }

        private IEnumerator SpawnEnemiesAfterDelay()
        {
            yield return new WaitForSeconds(SpawnDelay);
            SpawnEnemies();
        }

        private void SpawnEnemies()
        {
            var enemyLibrary = Libraries.EnemiesLibrary.GetByID(enemyId);
            var enemy = Resources.Load(enemyLibrary.Prefab);
            Instantiate(enemy, transform.position, Quaternion.identity);
        }
    }
}