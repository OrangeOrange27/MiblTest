using UnityEngine;

namespace System
{
    public class EndGameManager : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particle;
        [SerializeField] private AudioSource _audio;
        
        public void EndGame(Transform player)
        {
            _particle.transform.position = player.position;
            _particle.Play();
            
            _audio.Play();
            
            Destroy(player.gameObject);
        }
    }
}
