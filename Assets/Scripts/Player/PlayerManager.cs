using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private PlayerSpawner _playerSpawner;

        public PlayerController Player { get; private set; }

        public void KillPlayer()
        {
            Destroy(Player.gameObject);
        }

        public void SpawnPlayer()
        {
            Player = _playerSpawner.SpawnPlayer();
        }
    }
}
