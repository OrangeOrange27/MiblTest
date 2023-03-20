using System;
using UnityEngine;

namespace Obstacles
{
    public class ObstacleManager : MonoBehaviour, IDisposable
    {
        [SerializeField] private ObstacleSpawner _obstacleSpawner;

        public event Action OnPlayerCollisionEvent;
        
        public void StopSpawn()
        {
            _obstacleSpawner.StopSpawn();
        }
        public void StartSpawn()
        {
            _obstacleSpawner.StartSpawn();
        }
        
        public void DestroyAllObstacles()
        {
            foreach (var obstacle in _obstacleSpawner.SpawnedObjectsList)
            {
                Destroy(obstacle.gameObject);
            }

            _obstacleSpawner.ClearSpawnedObjectsList();
        }
        
        private void Awake()
        {
            _obstacleSpawner.OnObjectSpawned += OnObstacleSpawned;
        }

        private void OnObstacleSpawned(ObstacleBase obstacle)
        {
            obstacle.SetObstacleManager(this);
        }

        public void OnPlayerCollision()
        {
            OnPlayerCollisionEvent?.Invoke();
        }

        private void OnDestroy()
        {
            Dispose();
        }

        public void Dispose()
        {
            _obstacleSpawner.OnObjectSpawned -= OnObstacleSpawned;
        }
    }
}
