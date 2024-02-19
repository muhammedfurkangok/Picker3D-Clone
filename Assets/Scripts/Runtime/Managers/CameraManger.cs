using System;
using Cinemachine;
using JetBrains.Annotations;
using Runtime.Controllers.UI;
using Runtime.Signals;
using Unity.Mathematics;
using UnityEngine;

namespace Runtime.Managers
{
    public class CameraManger : MonoBehaviour
    {
        #region Self Veriables

        #region Serialized Veriables

        [SerializeField] [CanBeNull] private CinemachineVirtualCamera virtualCamera;
        
        #endregion

        #region Private Variables

        private float3 _firstPosition;

        #endregion

        #endregion

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            _firstPosition = transform.position;
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CameraSignals.Instance.onSetCameraTarget += OnSetCameraTarget;
            CoreGameSignals.Instance.onReset += OnReset;
        }

        private void OnReset()
        {
            transform.position = _firstPosition;
        }

        private void OnSetCameraTarget()
        {
            /*var player = FindObjectOfType<PlayerManager>().transform;
            virtualCamera.Follow = player;
            virtualCamera.LookAt = player;*/
        }

        private void UnSubscribeEvents()
        {
            CameraSignals.Instance.onSetCameraTarget -= OnSetCameraTarget;
            CoreGameSignals.Instance.onReset -= OnReset;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();;
        }
    }
}