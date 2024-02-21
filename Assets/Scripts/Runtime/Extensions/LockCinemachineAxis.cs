using Cinemachine;
using UnityEngine;

namespace Runtime.Extensions
{
    [ExecuteInEditMode]
    [SaveDuringPlay]
    [AddComponentMenu("")]
    public class LookCinemachineAxis : CinemachineExtension
    {
        [Tooltip("Kamerayı x ekseninde kitle")]
        public float m_XPosition = 0f;

        protected override void PostPipelineStageCallback(
            CinemachineVirtualCameraBase vcam,
            CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (stage == CinemachineCore.Stage.Body)
            {
                var pos = state.RawPosition;
                pos.x = m_XPosition;
                state.RawPosition = pos;
            }
        }
    }
}