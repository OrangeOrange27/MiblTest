using UnityEngine;

namespace System
{
    public class Factory<T>
        where T : MonoBehaviour
    {
        private readonly T _prefab;
        private readonly Transform _container;

        public Factory(T prefab, Transform container)
        {
            _prefab = prefab;
            _container = container;
        }

        public T Create()
        {
            var instance = UnityEngine.Object.Instantiate(_prefab, _container);
            return instance;
        }
    }
}
