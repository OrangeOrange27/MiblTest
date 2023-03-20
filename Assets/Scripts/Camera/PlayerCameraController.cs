using System;
using Player;
using UnityEngine;

namespace CameraScripts
{
    public class PlayerCameraController : MonoBehaviour
    {
        [SerializeField] private PlayerController _player;
        [SerializeField] private float _rotationSpeed;
        
        [SerializeField] private Transform _orientation;
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private Transform _playerModelTransform;

        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            var viewDir = _playerTransform.position -
                          new Vector3(_camera.transform.position.x, _playerTransform.position.y, _camera.transform.position.z);
            _orientation.forward = viewDir.normalized;
            
            var input = _player.LookAction.ReadValue<Vector2>();
            var inputDir = _orientation.forward * input.y + _orientation.right * input.x;

            _playerModelTransform.forward = Vector3.Slerp(_playerModelTransform.forward, inputDir.normalized,
                Time.deltaTime * _rotationSpeed);
        }
    }
}
