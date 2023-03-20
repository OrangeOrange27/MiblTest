using UnityEngine;

namespace Obstacles
{
    //abstraction to be able add new Obstacles easily
    public abstract class ObstacleBase : MonoBehaviour
    {
        private ObstacleManager _obstacleManager;

        public void SetObstacleManager(ObstacleManager obstacleManager)
        {
            _obstacleManager = obstacleManager;
        }

        protected virtual void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.CompareTag("Player"))
                return;

            _obstacleManager.OnPlayerCollision();
        }
    }
}
