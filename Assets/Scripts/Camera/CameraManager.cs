using Player;
using UnityEngine;

namespace CameraScripts
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private float _smoothness = .5f;

        private Camera _camera;
        private PlayerController _player;
        private Vector3 _cameraStartPos;
        private Vector3 _cameraOffset;

        public void SetPlayer(PlayerController player)
        {
            _player = player;

            ResetCameraPos();
            _cameraOffset = _camera.transform.position - _player.transform.position;
        }

        private void Awake()
        {
            _camera = Camera.main;
            _cameraStartPos = _camera.transform.position;
        }

        private void Update()
        {
            if (_player == null)
                return;

            MoveCamera();
        }

        private void ResetCameraPos()
        {
            _camera.transform.position = _cameraStartPos;
        }

        private void MoveCamera()
        {
            var newCameraPos = _player.transform.position + _cameraOffset;
            _camera.transform.position = Vector3.Slerp(_camera.transform.position, newCameraPos, _smoothness);
            _camera.transform.LookAt(_player.transform);
        }
    }
}
