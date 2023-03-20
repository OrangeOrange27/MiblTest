using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace System
{
    public class SpawnerBase<TSpawnObject, TConfiguration> : MonoBehaviour
        where TSpawnObject : MonoBehaviour
        where TConfiguration : BaseSpawnConfiguration
    {
        [SerializeField] private TConfiguration _spawnConfiguration;
        [SerializeField] private TSpawnObject _prefab;
        [SerializeField] private Transform _spawnedObjectsContainer;

        protected Factory<TSpawnObject> _factory;
        protected List<TSpawnObject> _spawnedObjectsList;
        protected Coroutine _spawnCoroutine;

        public List<TSpawnObject> SpawnedObjectsList => _spawnedObjectsList;

        public void StartSpawn() => _spawnCoroutine = StartCoroutine(SpawnCoroutine());
        public void StopSpawn() => StopCoroutine(_spawnCoroutine);
        public void ClearSpawnedObjectsList() => _spawnedObjectsList = new List<TSpawnObject>();

        private void Awake()
        {
            _factory = new Factory<TSpawnObject>(_prefab, _spawnedObjectsContainer, _spawnConfiguration.SpawnRadius);

            _spawnedObjectsList = new List<TSpawnObject>();

            _spawnCoroutine = StartCoroutine(SpawnCoroutine());
        }

        private IEnumerator SpawnCoroutine()
        {
            yield return new WaitForSeconds(_spawnConfiguration.SpawnDelay);
            SpawnInternal();
            if (_spawnedObjectsList.Count != _spawnConfiguration.SpawnCount)
                _spawnCoroutine = StartCoroutine(SpawnCoroutine());
        }

        protected virtual TSpawnObject SpawnInternal()
        {
            var spawnedObject = _factory.Create();
            _spawnedObjectsList.Add(spawnedObject);
            return spawnedObject;
        }
    }
}
