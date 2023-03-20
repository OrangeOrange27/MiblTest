using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private float _playerSpeed = 10f;
        [SerializeField] private float _smoothInputSpeed = .2f;

        public InputAction MoveAction { get; private set; }
        public InputAction LookAction { get; private set; }

        private Vector2 _currentInputVector;
        private Vector2 _smoothInputVelocity;

        private void Awake()
        {
            MoveAction = _playerInput.actions["Move"];
            LookAction = _playerInput.actions["Look"];
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            var input = MoveAction.ReadValue<Vector2>();
            _currentInputVector = Vector2.SmoothDamp(_currentInputVector, input, ref _smoothInputVelocity, _smoothInputSpeed);
            
            var moveVector = new Vector3(_currentInputVector.x, 0, _currentInputVector.y);
            moveVector *= _playerSpeed * Time.deltaTime;
            transform.Translate(moveVector,Space.World);
        }
    }
}
