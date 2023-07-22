using System.Collections;
using DG.Tweening;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class DeathScreen : MonoBehaviour
    {
        [SerializeField] private GameObject deathScreen;
        [SerializeField] private float blackDelay;
        [SerializeField] private Image blackScreen;
        [SerializeField] private float fadeDuration;
        [SerializeField] private AudioSource source;
        [SerializeField] private AudioClip clip;
        private PlayerComposer _player;
        [Inject]
        private void Construct(PlayerComposer player)
        {
            _player = player;
            player.health.dead.AddListener(p => StartCoroutine(Death()));
        }

        private IEnumerator Death()
        {
            source.volume = 1;
            PlayerInputs.Instance.Game.Disable();
            deathScreen.SetActive(true);
            blackScreen.gameObject.SetActive(true);
            _player.animations.Death();
            source.PlayOneShot(clip);
            yield return new WaitForSeconds(blackDelay);
            blackScreen.DOColor(Color.black, fadeDuration);
            yield return new WaitForSeconds(fadeDuration);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}