using System;
using Runtime.Enums;
using Runtime.Managers;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Runtime.Handlers
{
    public class UIEventSubscriber:MonoBehaviour
    {
        #region Self Veriables

        #region Serialized Veriables

        [SerializeField] private UIEventSubscriptionTypes type;
        [SerializeField] private Button button;

        #endregion

        #region Private Variables

        private UIManager _manager; //*


        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
        }

        private void GetReferences()
        {
            _manager = FindObjectOfType<UIManager>();
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            switch (type)
            {
             case UIEventSubscriptionTypes.OnPlay:
                 button.onClick.AddListener(_manager.Play);
                 break;
             case UIEventSubscriptionTypes.OnNextLevel:
                 button.onClick.AddListener(_manager.NextLevel);
                 break;
             case UIEventSubscriptionTypes.OnRestartLevel:
                 button.onClick.AddListener(_manager.RestartLevel);
                 break;
             default:
                 throw new ArgumentException();
            }
        }

        private void UnSubscribeEvents()
        {
            switch (type)
            {
                case UIEventSubscriptionTypes.OnPlay:
                    button.onClick.RemoveListener(_manager.Play);
                    break;
                case UIEventSubscriptionTypes.OnNextLevel:
                    button.onClick.RemoveListener(_manager.NextLevel);
                    break;
                case UIEventSubscriptionTypes.OnRestartLevel:
                    button.onClick.RemoveListener(_manager.RestartLevel);
                    break;
                default:
                    throw new ArgumentException();
            } 
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
            ;
        }
    }
}