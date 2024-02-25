using System;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class CoreGameSignals : MonoBehaviour
    {
        #region Singleton

        public static CoreGameSignals Instance;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        #endregion

        
        public UnityAction<byte> onLevelInitialize = delegate { };
        public UnityAction onClearActiveLevel = delegate { };
        public UnityAction onLevelSuccessful = delegate { };
        public UnityAction onLevelFailed = delegate { };
        public UnityAction onNextLevel = delegate { };
        public UnityAction onRestartLevel = delegate { };
        public UnityAction onPlay = delegate { };
        public UnityAction onReset = delegate { };
        public Func<byte> onGetLevelValue = delegate { return 0; };

        public UnityAction<byte> onStageAreaSuccessful = delegate { };
        public UnityAction onStageAreaEntered = delegate { };
        public UnityAction onFinishAreaEntered = delegate { };
        public UnityAction onMiniGameAreaEntered = delegate { };
        public UnityAction onMultiplierAreaEntered = delegate { };


    }
}