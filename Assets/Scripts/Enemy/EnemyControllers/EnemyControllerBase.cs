using UnityEngine;

namespace Enemy
{
    public abstract class EnemyControllerBase : MonoBehaviour, IEnemyController
    {
        [SerializeField] protected float _speed;

        protected Transform _target;
        protected EnemyManager _enemyManager;

        public Transform Target
        {
            get => _target;
            set => _target = value;
        }

        public void SetEnemyManager(EnemyManager enemyManager) => _enemyManager = enemyManager;

        protected void FixedUpdate()
        {
            TryMovePlayer();
        }

        protected bool TryMovePlayer()
        {
            if (_target == null)
                return false;

            transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);

            return true;
        }

        protected virtual void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.CompareTag("Player"))
                return;

            Target = null;
            _enemyManager.EnemyReachedPlayer();
        }
    }
}