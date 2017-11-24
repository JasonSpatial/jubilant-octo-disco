namespace Assets.Scripts
{
    using UnityEngine;
    using UnityEngine.Assertions;

    /// <summary>
    ///     Handles spawning the enemies at the start of the level.
    /// </summary>
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private Transform enemy1Prefab = null;

        [SerializeField]
        private Transform enemy2Prefab = null;

        [SerializeField]
        private Transform enemy3Prefab = null;

        [SerializeField]
        private float totalSpawnTime = 1;

        [SerializeField]
        private int enemyColumns = 11;

        [SerializeField]
        private int enemyRows = 4;

        [SerializeField]
        private float enemyHorizontalSpacing = 1.1f;

        [SerializeField]
        private float enemyVerticalSpacing = 1.1f;

        private float timeSinceLastEnemySpawn = 0;

        private Vector3 nextEnemySpawnPosition;

        private int nextEnemyColumn = 0;

        private int nextEnemyRow = 0;

        private bool spawningEnemies = true;

        private Transform currentEnemyPrefab;

        private Transform enemyStartPosition;

        private int MaxEnemyCount
        {
            get { return enemyColumns*enemyRows; }
        }

        private float SpawnDelay
        {
            get { return totalSpawnTime/MaxEnemyCount; }
        }

        private void Start()
        {
            Assert.IsTrue(enemy1Prefab != null, "No type 1 enemy prefab was provided.");
            Assert.IsTrue(enemy2Prefab != null, "No type 2 enemy prefab was provided.");
            Assert.IsTrue(enemy3Prefab != null, "No type 3 enemy prefab was provided.");

            GameObject enemyStartPositionGO = GameObject.FindGameObjectWithTag(Tags.EnemyStartPosition);
            Assert.IsTrue(enemyStartPositionGO != null, "Could not find the grid position.");
            enemyStartPosition = enemyStartPositionGO.transform;
            nextEnemySpawnPosition = enemyStartPosition.position;

            currentEnemyPrefab = enemy1Prefab;
        }

        private void Update()
        {
            if (spawningEnemies)
            {
                timeSinceLastEnemySpawn += Time.fixedDeltaTime;
                while (timeSinceLastEnemySpawn >= SpawnDelay)
                {
                    timeSinceLastEnemySpawn -= SpawnDelay;

                    Instantiate(currentEnemyPrefab, nextEnemySpawnPosition, Quaternion.identity);
                    nextEnemySpawnPosition += new Vector3(enemyHorizontalSpacing, 0, 0);
                    nextEnemyColumn++;
                    if (nextEnemyColumn >= enemyColumns)
                    {
                        nextEnemyRow++;
                        if (nextEnemyRow >= enemyRows)
                        {
                            spawningEnemies = false;
                        }
                        else
                        {
                            nextEnemyColumn = 0;
                            nextEnemySpawnPosition = new Vector3(enemyStartPosition.position.x, nextEnemySpawnPosition.y - enemyVerticalSpacing, 0);
                            if (nextEnemyRow == 1)
                            {
                                currentEnemyPrefab = enemy2Prefab;
                            }
                            else if (nextEnemyRow >= 2)
                            {
                                currentEnemyPrefab = enemy3Prefab;
                            }
                        }
                    }
                }
            }
        }
    }
}