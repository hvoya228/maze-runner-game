using System;
using System.Collections;
using Controllers.InGameControllers;
using DG.Tweening;
using Models.Enemies.Interfaces;
using Pooling.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Models.Enemies
{
    public class Trap : MonoBehaviour, IPoolAble, IEnemy
    {
        [SerializeField] private Collider2D trapCollider;
        [SerializeField] private SpriteRenderer spriteRenderer;

        private Coroutine _appearingCoroutine;

        public GameObject GameObject => gameObject;
        
        public event Action<IPoolAble> OnDestroyed;

        private void OnEnable()
        {
            LevelFinisher.OnLevelFinished += Reset;
            LevelFinisher.OnGameFinished += Reset;
        }

        private void OnDisable()
        {
            LevelFinisher.OnLevelFinished -= Reset;
            LevelFinisher.OnGameFinished -= Reset;
        }
        
        public void CaughtPlayer()
        {
            Reset();
        }

        public void Reset()
        {
            StopAppearingAnimation();
            OnDestroyed?.Invoke(this);
        }
        
        private void StopAppearingAnimation()
        {
            if (_appearingCoroutine == null) return;
            StopCoroutine(_appearingCoroutine);
            _appearingCoroutine = null;
        }

        public void PlayAppearingAnimation()
        {
            _appearingCoroutine = StartCoroutine(AppearingAnimationCoroutine());
        }

        // ReSharper disable once FunctionRecursiveOnAllPaths
        private IEnumerator AppearingAnimationCoroutine()
        {
            yield return new WaitForSeconds(Random.Range(1.5f, 3f));
            MakeActive(false);
            yield return new WaitForSeconds(Random.Range(1.5f, 3f));
            MakeActive(true);
            StartCoroutine(AppearingAnimationCoroutine());
        }

        private void MakeActive(bool isActive)
        {
            spriteRenderer.DOFade(isActive ? 1 : 0, 0.3f);
            trapCollider.enabled = isActive;
        }
    }
}
