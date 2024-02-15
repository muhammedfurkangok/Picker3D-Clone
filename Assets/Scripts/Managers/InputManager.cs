using System;
using System.Collections.Generic;
using Data.UnityObjects;
using Data.ValueObjects;
using Keys;
using Signals;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        #region Self Veriables

        #region Private Veriables

        private InputData _data;
        private bool _isAvailableForTouch,  _isFirstTimeTouchTaken, _isTouching;

        private float _currentVelocity;
        private float3 _moveVector;
        private Vector2? _mousePosition;
        




        #endregion

        #endregion

        private void Awake()
        {
            _data = GetInputData();
        }

        private InputData GetInputData()
        {
            return Resources.Load<CD_Input>("Data/CD_Input").Data;
        }
#region Observer Pattern
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onReset += OnReset;
            InputSignals.Instance.onEnableInput += OnEnableInput;
            InputSignals.Instance.onDisableInput += OnDisableInput;
            //InputSignals.Instance.onEnableInput += OnInputStateChanged;
        }
                    
        /*
        private void OnInputStateChanged(bool state )
        {
            _isAvailableForTouch = state;
        }
        ikinci bir çözümdür ve state machinelerle beraber çözülebilir ileri seviye bir çözümdür
        */
        
       

        private void OnReset()
        {
           // _isFirstTimeTouchTaken = false; oyun ilk açıldıgında bir tanıtım için bir şey tutorialın bir kere gözükümesi yeterli her level öncesi göstermenin mantıgı yok 
           
            _isAvailableForTouch = false;
            _isTouching = false;
        }

        private void UnsubscribeEvents()
        {
            
            CoreGameSignals.Instance.onReset -= OnReset;
            InputSignals.Instance.onEnableInput -= OnEnableInput;
            InputSignals.Instance.onDisableInput -= OnDisableInput;
        }

        private void OnDisableInput()
        {
            _isAvailableForTouch = true;
        }

        private void OnEnableInput()
        {

            _isAvailableForTouch = true;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
            //ödev:burdan sonra update de yazılacak levelmanagerdeki parçalama udpatede çok fazla iş yapılmaması içindi benzer şekilde updatedeki kısmı parçalamayı denememiz gerekiuot
            //bu sadece yatay düzlemde input alınan bir sistem her bir platformda(klavye,mouse vb) oluşturulabilmesi için input analiz sistemi yazılmalı
            //
        }
        #endregion

        private void Update()
        {
            if (!_isAvailableForTouch) return;

            if (Input.GetMouseButtonUp(0) && !IsPointerOverUIElement())
            {
                _isTouching = false;
                InputSignals.Instance.onInputReleased?.Invoke();
                Debug.Log("Executed ------>>> OnInputTaken");
                
            }

            if (Input.GetMouseButtonDown(0) && IsPointerOverUIElement())
            {
                _isTouching = true;
                InputSignals.Instance.onInputTaken?.Invoke();
                Debug.Log("Executed ------>>> OnInputTaken");
                if (_isFirstTimeTouchTaken)
                {
                    _isTouching = true;
                    InputSignals.Instance.onFirstTimeTouchTaken?.Invoke();
                    Debug.Log("Executed ------>>> OnFirstTimeTouchTaken");
                }

                _mousePosition = Input.mousePosition;
                if (Input.GetMouseButton(0) && !IsPointerOverUIElement())
                {
                    if (_isTouching)
                    {
                        if (_mousePosition != null)
                        {
                            Vector2 mouseDeltaPos = (Vector2)Input.mousePosition - _mousePosition.Value;
                            if (mouseDeltaPos.x > _data.HorizontalInputSpeed)
                            {
                                _moveVector.x = _data.HorizontalInputSpeed / 10f * mouseDeltaPos.x;
                            }
                            else if (mouseDeltaPos.x < _data.HorizontalInputSpeed)
                            {
                                _moveVector.x = -_data.HorizontalInputSpeed / 10f * mouseDeltaPos.x;
                            }
                            else
                            {
                                _moveVector.x = Mathf.SmoothDamp(-_moveVector.x, 0f, ref _currentVelocity,
                                    _data.ClampSpeed); //???? yavaş yavaş durmadan bahsediyor sanırım
                            }
                        }

                        _mousePosition = Input.mousePosition;

                        InputSignals.Instance.onInputDragged?.Invoke(new HorizontalInputParams()
                        {
                            HorizontalValue = _moveVector.x,
                            ClampValues = _data.ClampValues
                        });
                    }
                }
            }
        }

        private bool IsPointerOverUIElement()
        {
            //????????
            var eventData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            return results.Count > 0;
        }
    }
}