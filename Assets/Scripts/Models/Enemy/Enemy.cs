using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class Enemy : MonoBehaviour, IPoolable
{
    [SerializeField] private EnemyTriggerZone triggerZone;
    [SerializeField] private Sprite activeEnemySprite;
    [SerializeField] private Sprite sleepingEnemySprite;
    [SerializeField] private ParticleSystem sleepingEffectParticle;

    private float speed;
    private bool isSeePlayer;
    private bool isReachedPlayer;
    private SpriteRenderer spriteRenderer;

    public GameObject GameObject => gameObject;

    public static event Action OnReachedPlayer;
    public static event Action OnPlayerInDangeroues;
    public static event Action OnEndOfPlayerDangeroues;
    public event Action<IPoolable> OnDestroyed;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (isSeePlayer)
        {
            FollowPlayer();
            CheckPlayerReaching();
            MakeTriggerZoneFading();
        }
    }

    private void OnEnable()
    {
        triggerZone.OnTriggerEnter += WakeUpEnemy;
        FinishLevelController.OnGameFinished += StopParticleEffect;
        FinishLevelController.OnLevelFinished += StopParticleEffect;
        FinishLevelController.OnLevelFinished += Reset;
    }

    private void OnDisable()
    {
        triggerZone.OnTriggerEnter -= WakeUpEnemy;
        FinishLevelController.OnGameFinished -= StopParticleEffect;
        FinishLevelController.OnLevelFinished -= StopParticleEffect;
        FinishLevelController.OnLevelFinished -= Reset;
    }

    public void Reset()
    {
        MakeEnemySleep();
        OnDestroyed?.Invoke(this);
    }

    private void WakeUpEnemy()
    {
        isSeePlayer = true;
        sleepingEffectParticle.Stop();
        spriteRenderer.sprite = activeEnemySprite;
        Debug.Log("detected player");
    }

    public void MakeEnemySleep()
    {
        isSeePlayer = false;
        isReachedPlayer = false;
        sleepingEffectParticle.Play();
        triggerZone.SetAlphaOfColor(0.15f);
        spriteRenderer.sprite = sleepingEnemySprite;
        triggerZone.StartBreathingAnimation();
    }

    private float GetDistanceToPlayer()
    {
        return Vector2.Distance(transform.position, Player.Position);
    }

    private void FollowPlayer()
    {
        transform.position = Vector3.Lerp(transform.position, Player.Position, speed * Time.deltaTime);
    }

    private void CheckPlayerReaching()
    {
        if (GetDistanceToPlayer() <= 0.4f && !isReachedPlayer)
        {
            OnReachedPlayer?.Invoke();
            isReachedPlayer = true;
        }
    }

    private void MakeTriggerZoneFading()
    {
        if (GetDistanceToPlayer() <= 3f)
        {
            speed = 0.3f;
            triggerZone.SetAlphaOfColor(1f);
            OnPlayerInDangeroues?.Invoke();
        }
        else if (GetDistanceToPlayer() <= 5f)
        {
            speed = 0.2f;
            triggerZone.SetAlphaOfColor(0.75f);
            OnEndOfPlayerDangeroues?.Invoke();
        }
        else if (GetDistanceToPlayer() > 5f)
        {
            speed = 0.1f;
            triggerZone.SetAlphaOfColor(0);
        }
    }

    private void StopParticleEffect()
    {
        sleepingEffectParticle.Stop();
    }
}
