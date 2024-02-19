using System;
using Runtime.Keys;
using Runtime.Managers;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Veriables

        [SerializeField] private new Rigidbody rigidboy;
                
        

        #endregion

        #region Private Variables

        [ShowInInspector] private PlayerMovementData _data;
        [ShowInInspector] private bool _isReadyToMove,_isReadyToPlay;
        [ShowInInspector] private float _xValue;

        private float2 _clampValues;

        #endregion

        #endregion

        internal void SetData(PlayerMovementData data)
        {
            _data = data;
        }

        private void FixedUpdate()
        {
            if (!_isReadyToPlay)
            {
                StopPlayer();
                return;
            }

            if (!_isReadyToMove)
            {
                MovePlayer();
            }
            else
            {
                StopPlayerHorizontally();
            }
        }

        private void StopPlayer()
        {
            rigidboy.velocity = Vector3.zero;
            rigidboy.angularVelocity = Vector3.zero;
        }

        private void StopPlayerHorizontally()
        {
            rigidboy.velocity = new Vector3(0,rigidboy.velocity.y,_data.ForwardSpeed);
            rigidboy.angularVelocity = Vector3.zero;
        }

        private void MovePlayer()
        {
            var velocity = rigidboy.velocity;
            velocity = new Vector3(_xValue * _data.SidewaySpeed, velocity.y, _data.ForwardSpeed);
            rigidboy.velocity = velocity;
            var position1 = rigidboy.position;
            Vector3 position;
            position = new Vector3(Mathf.Clamp(position1.x, _clampValues.x, _clampValues.y),
                (position = rigidboy.position).y, position.z);
            rigidboy.position = position;
        }

        internal void IsReadyToPlay(bool condintion)
        {
            _isReadyToPlay = condintion;
        }

        internal void IsReadyToMove(bool condintion)
        {
            _isReadyToMove = condintion;
        }

        internal void UpdateParams(HorizontalInputParams inputParams)
        {
            _xValue = inputParams.HorizontalValue;
            _clampValues = inputParams.ClampValues;
        }

        internal void OnReset()
        {
            StopPlayer();
            _isReadyToMove = false;
            _isReadyToPlay = false;
        }

        public void UpdateInputParams(HorizontalInputParams inputParams)
        {
            //.
        }
    }
}