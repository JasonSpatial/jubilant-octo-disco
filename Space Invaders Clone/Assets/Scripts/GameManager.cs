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
        
        private Transform gridPosition;

        private void Start()
        {
            Assert.IsTrue(enemy1Prefab != null, "No type 1 enemy prefab was provided.");

            GameObject gridPositionGO = GameObject.FindGameObjectWithTag(Tags.EnemyStartPosition);
            Assert.IsTrue(gridPositionGO != null, "Could not find the grid position.");
            gridPosition = gridPositionGO.transform;

            Instantiate(enemy1Prefab, gridPosition.position, Quaternion.identity);
        }
    }
}