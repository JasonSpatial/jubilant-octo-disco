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

        private int nextEnemyColumn = 0;

        private int nextEnemyRow = 0;

        private Transform enemyStartPosition;

        private int MaxEnemyCount
        {
            get { return enemyColumns*enemyRows; }
        }

        private float SpawnDelay
        {
            get { return totalSpawnTime/MaxEnemyCount; }
        }

        private Transform CurrentEnemyPrefab
        {
            get
            {
                if (nextEnemyRow == 0)
                {
                    return enemy1Prefab;
                }
                else if (nextEnemyRow == 1)
                {
                    return enemy2Prefab;
                }
                else
                {
                    return enemy3Prefab;
                }
            }
        }

        private Vector3 CurrentEnemySpawnPosition
        {
            get { return enemyStartPosition.position + new Vector3(nextEnemyColumn*enemyHorizontalSpacing, -nextEnemyRow*enemyVerticalSpacing); }
        }

        private void Start()
        {
            Assert.IsTrue(enemy1Prefab != null, "No type 1 enemy prefab was provided.");
            Assert.IsTrue(enemy2Prefab != null, "No type 2 enemy prefab was provided.");
            Assert.IsTrue(enemy3Prefab != null, "No type 3 enemy prefab was provided.");

            GameObject enemyStartPositionGO = GameObject.FindGameObjectWithTag(Tags.EnemyStartPosition);
            Assert.IsTrue(enemyStartPositionGO != null, "Could not find the grid position.");
            enemyStartPosition = enemyStartPositionGO.transform;
        }

        private void Update()
        {
            timeSinceLastEnemySpawn += Time.deltaTime;
            while (timeSinceLastEnemySpawn >= SpawnDelay)
            {
                timeSinceLastEnemySpawn -= SpawnDelay;

                Instantiate(CurrentEnemyPrefab, CurrentEnemySpawnPosition, Quaternion.identity);
                nextEnemyColumn++;
                if (nextEnemyColumn >= enemyColumns)
                {
                    nextEnemyRow++;
                    nextEnemyColumn = 0;

                    if (nextEnemyRow >= enemyRows)
                    {
                        enabled = false;
                    }
                }
            }
        }
    }
}