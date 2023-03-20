using UnityEngine;

namespace System
{
    public class SpawnerWithEvent<TSpawnObject, TConfiguration> : SpawnerBase<TSpawnObject, TConfiguration>
        where TSpawnObject : MonoBehaviour
        where TConfiguration : BaseSpawnConfiguration
    {
        public event Action<TSpawnObject> OnObjectSpawned;

        protected override TSpawnObject SpawnInternal()
        {
            var spawnedObject = _factory.Create();
            _spawnedObjectsList.Add(spawnedObject);

            OnObjectSpawned?.Invoke(spawnedObject);

            return spawnedObject;
        }
    }
}

