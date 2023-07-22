using System;
using System.Collections;
using DG.Tweening;
using Player;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class FadeOut : MonoBehaviour
    {
        [SerializeField] private Image blackScreen;
        [SerializeField] private float duration;
        [SerializeField] private AudioSource source;
        [SerializeField] private AudioClip clip;
        private PlayerAnimationController _player;

        [Inject]
        private void Construct(PlayerComposer player)
        {
            _player = player.GetComponent<PlayerAnimationController>();
        }
        private void Start()
        {
            StartCoroutine(Fade());
            //_player.Respawn();
        }

        private IEnumerator Fade()
        {
            source.clip = clip;
            source.Play();
            source.DOFade(0, 9);
            blackScreen.DOColor(Color.clear, duration);
            yield return new WaitForSeconds(duration);
            blackScreen.gameObject.SetActive(false);
        }
    }
}