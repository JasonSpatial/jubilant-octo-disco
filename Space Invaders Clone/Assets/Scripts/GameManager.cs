namespace Assets.Scripts
{
    using UnityEngine;
    using UnityEngine.Assertions;

    /// <summary>
    ///     General game code that does not belong anywhere else.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private Transform enemy1Prefab = null;

        [SerializeField]
        private float totalSpawnTime = 1;

        [SerializeField]
        private int enemiesPerRow = 11;

        [SerializeField]
        private float enemyHorizontalSpacing = 1.1f;

        private float timeSinceLastEnemySpawn = 0;

        private Vector3 nextEnemySpawnPosition;

        private int enemiesSpawned = 0;

        private bool spawningEnemies = true;

        private int MaxEnemyCount
        {
            get { return enemiesPerRow; }
        }

        private float SpawnDelay
        {
            get { return totalSpawnTime/MaxEnemyCount; }
        }

        private void Start()
        {
            Assert.IsTrue(enemy1Prefab != null, "No type 1 enemy prefab was provided.");

            GameObject enemyStartPositionGO = GameObject.FindGameObjectWithTag(Tags.EnemyStartPosition);
            Assert.IsTrue(enemyStartPositionGO != null, "Could not find the grid position.");
            Transform enemyStartPosition = enemyStartPositionGO.transform;
            nextEnemySpawnPosition = enemyStartPosition.position;
        }

        private void FixedUpdate()
        {
            if (spawningEnemies)
            {
                timeSinceLastEnemySpawn += Time.fixedDeltaTime;
                while (timeSinceLastEnemySpawn >= SpawnDelay)
                {
                    timeSinceLastEnemySpawn -= SpawnDelay;

                    Instantiate(enemy1Prefab, nextEnemySpawnPosition, Quaternion.identity);
                    nextEnemySpawnPosition += new Vector3(enemyHorizontalSpacing, 0, 0);
                    enemiesSpawned++;
                    if (enemiesSpawned >= MaxEnemyCount)
                    {
                        spawningEnemies = false;
                    }
                }
            }
        }
    }
}