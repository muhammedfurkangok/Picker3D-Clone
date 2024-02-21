using DG.Tweening;
using Runtime.Data.ValueObjects;
using Runtime.Managers;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerMeshController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Veriables

        [SerializeField] private PlayerManager manager;
        [SerializeField] private new Renderer renderer;
        [SerializeField] private TextMeshPro scaleText;
        [SerializeField] private ParticleSystem confetti;

        #endregion

        #region Private Variables

        [ShowInInspector] private PlayerMeshData _data;
        [ShowInInspector] private PoolData _poolData;

        #endregion

        #endregion

        internal void SetData(PlayerMeshData scaleData)
        {
        _data = scaleData;
        }

        internal void ScaleUpPlayer()
        {
            renderer.gameObject.transform.DOScaleX(_data.ScaleCounter, 1).SetEase(Ease.Flash);
        }

        internal void ShowUpText()
        {
            //bittikten sonra animasyon yeni bir animasyon gibi düşün 
            scaleText.gameObject.SetActive(true);
            scaleText.DOFade(1, 0f).SetEase(Ease.Flash).OnComplete(() => scaleText.DOFade(0, 0).SetDelay(.65f));
            scaleText.rectTransform.DOAnchorPosY(.85f, .65f).SetRelative(true).SetEase(Ease.OutBounce).OnComplete(() =>
                scaleText.rectTransform.DOAnchorPosY(-.85f, .65f).SetRelative(true));

        }

        internal void PlayConfetti()
        {
            confetti.Play();
          
        }

        internal void OnReset()
        {
            renderer.gameObject.transform.DOScaleX(1, 1).SetEase(Ease.Linear);
        }
        
    }
}