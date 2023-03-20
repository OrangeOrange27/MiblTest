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
        private Coroutine _spawnCoroutine;

        public List<TSpawnObject> SpawnedObjectsList => _spawnedObjectsList;

        public void StartSpawn() => _spawnCoroutine = StartCoroutine(SpawnCoroutine());
        public void StopSpawn() => StopCoroutine(_spawnCoroutine);

        public void ClearSpawnedObjectsList()
        {
            foreach (var obj in _spawnedObjectsList)
            {
                obj.gameObject.SetActive(false);
            }
        }

        private void Awake()
        {
            _factory = new Factory<TSpawnObject>(_prefab, _spawnedObjectsContainer);

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

        protected void PositionInstance(Transform instanceTransform)
        {
            var randomPos = UnityEngine.Random.onUnitSphere * _spawnConfiguration.SpawnRadius;
            randomPos.y = .5f;
            instanceTransform.position = randomPos;
        }
        
        protected virtual TSpawnObject SpawnInternal()
        {
            var spawnedObject = TrySpawnFreeObjectFromPool(out var isFreeObjectInPool);

            if (!isFreeObjectInPool)
            {
                spawnedObject = _factory.Create();
                _spawnedObjectsList.Add(spawnedObject);
            }

            PositionInstance(spawnedObject.transform);
            return spawnedObject;
        }

        protected TSpawnObject TrySpawnFreeObjectFromPool(out bool isFreeObjectInPool)
        {
            foreach (var obj in _spawnedObjectsList)
            {
                if (obj.gameObject.activeInHierarchy) continue;
                
                obj.gameObject.SetActive(true);
                
                isFreeObjectInPool = true;
                return obj;
            }

            isFreeObjectInPool = false;
            return null;
        }
    }
}
