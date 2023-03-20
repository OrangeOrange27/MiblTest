using System;
using Player;
using UnityEngine;

namespace Enemy
{
    public class EnemyManager : MonoBehaviour, IDisposable
    {
        [SerializeField] private PlayerManager _playerManager;
        [SerializeField] private EnemySpawner _enemySpawner;

        public event Action OnEnemyReachPlayerEvent;

        private void Awake()
        {
            _enemySpawner.OnObjectSpawned += OnEnemySpawned;
        }

        private void OnEnemySpawned(EnemyControllerBase enemy)
        {
            enemy.SetEnemyManager(this);
            enemy.Target = _playerManager.Player.transform;
        }

        public void EnemyReachedPlayer()
        {
            OnEnemyReachPlayerEvent?.Invoke();
        }

        public void StopEnemySpawn()
        {
            _enemySpawner.StopSpawn();
        }
        public void StartEnemySpawn()
        {
            _enemySpawner.StartSpawn();
        }
        public void KillAllEnemies()
        {
            _enemySpawner.ClearSpawnedObjectsList();
        }

        private void OnDestroy()
        {
            Dispose();
        }

        public void Dispose()
        {
            _enemySpawner.OnObjectSpawned -= OnEnemySpawned;
        }
    }
}
