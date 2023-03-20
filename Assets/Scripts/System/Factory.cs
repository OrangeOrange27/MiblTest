using UnityEngine;

namespace System
{
    public class Factory<T> : IFactory<T>
        where T : MonoBehaviour
    {
        private readonly T _prefab;
        private readonly Transform _container;
        private readonly float _spawnRadius;

        public Factory(T prefab, Transform container, float spawnRadius)
        {
            _prefab = prefab;
            _container = container;
            _spawnRadius = spawnRadius;
        }

        public T Create()
        {
            var instance = UnityEngine.Object.Instantiate(_prefab, _container);
            PositionInstance(instance.transform);
            return instance;
        }

        private void PositionInstance(Transform instanceTransform)
        {
            var randomPos = UnityEngine.Random.onUnitSphere * _spawnRadius;
            randomPos.y = .5f;
            instanceTransform.position = randomPos;
        }
    }
}
