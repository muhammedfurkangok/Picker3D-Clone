using System;
using DG.Tweening;
using Runtime.Controllers.Pool;
using Runtime.Managers;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerPhysicController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Veriables

        [SerializeField] private PlayerManager manager;
        [SerializeField] private new Collider collider;
        [SerializeField] private new Rigidbody rigidbody;

        #endregion

        #region Private Variables

        private readonly string _stageArea = "StageArea";
        private readonly string _finish = "FinishArea";
        private readonly string _miniGame = "MiniGameArea";

        #endregion

        #endregion
        

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_stageArea))
            {
                manager.ForceCommand.Execute();
                CoreGameSignals.Instance.onStageAreaEntered?.Invoke();
                InputSignals.Instance.onDisableInput?.Invoke();
                
                //Stage Area Kontrol Süresi
                DOVirtual.DelayedCall(3, (() =>
                {
                    //3 saniyelik işlem içerisinde poolcontrollerdan cevap alacağım bu sayede devam mı tamam mı belirleyeeceğim
                    var result = other.transform.parent.GetComponentInChildren<PoolController>()
                        .TakeResults(manager.StageValue);

                    if (result)
                    {
                        CoreGameSignals.Instance.onStageAreaSuccessful?.Invoke(manager.StageValue);
                        InputSignals.Instance.onEnableInput?.Invoke();
                    }
                    else
                    
                        CoreGameSignals.Instance.onLevelFailed?.Invoke();
                    
                }));
                return;
            }

          
            if (other.CompareTag(_finish))
            {
                CoreGameSignals.Instance.onFinishAreaEntered?.Invoke();
                InputSignals.Instance.onDisableInput?.Invoke();
                return;
            }


            if (other.CompareTag(_miniGame))
            {
                //minigame dk 42 ders 21
            }
        }

      

        public void OnReset()
        {
            
        }
    }
}