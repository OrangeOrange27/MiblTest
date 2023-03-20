using UnityEngine;

namespace Player
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerPrefab;
        [SerializeField] private Transform _playerContainer;

        public PlayerController SpawnPlayer()
        {
            return Instantiate(_playerPrefab,_playerContainer);
        }
    }
}
