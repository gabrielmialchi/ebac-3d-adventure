using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using Animation;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour, IDamageable
    {
        //Public References
        [Header("Components")]
        public Collider enemyCollider;
        public FlashColor flashColor;
        public ParticleSystem damageParticleSystem;
        public AudioSource enemyAS;
        [Space]

        //Public Variables
        [Header("Health")]
        public int startLife = 10;
        public int maxLife = 100;
        public int _currentLife;
        [Space]

        [Header("Damage")]
        public int damage = 2;
        public bool lookAtPlayer = false;
        [Space]

        [Header("Sounds")]
        public AudioClip damageSound;
        public AudioClip crySound;
        public AudioClip deathSound;
        [Space]

        [Header("Spawn Animation")]
        public float spawnAnimationDuration = .2f;
        public Ease spawnAnimationEase = Ease.OutBack;
        public bool startWithSpawnAnimation = true;
        [Space]

        //Private References
        [Header("Animation")]
        [SerializeField] private AnimationBase _animationBase;
        private PlayerBase _player;

        //Private Variables
        [SerializeField] private float _delayToDestroy = 1f;
        [Space]

        //Events
        [Header("Events")]
        public UnityEvent OnKillEvent;
        //[Space]

        private void Awake()
        {
            Init();
        }

        private void Start()
        {
            _player = GameObject.FindObjectOfType<PlayerBase>();
        }

        protected virtual void Init()
        {
            ResetLife();
            if (startWithSpawnAnimation) SpawnAnimation();
            enemyAS.Play();
        }

        protected virtual void ResetLife()
        {
            _currentLife = startLife;
        }

        protected virtual void Kill()
        {
            OnKill();
        }

        protected virtual void OnKill()
        {
            if (enemyCollider != null) enemyCollider.enabled = false;
            Destroy(gameObject, _delayToDestroy);
            PlayAnimationByTrigger(AnimationType.DEATH);
            OnKillEvent?.Invoke();
        }

        public void OnDamage(int damage)
        {
            if (flashColor != null) flashColor.Flash();
            if (damageParticleSystem != null) damageParticleSystem.Play();

            //transform.position -= transform.forward;

            _currentLife -= damage;

            enemyAS.PlayOneShot(damageSound);

            if (_currentLife <= 0)
            {
                enemyAS.PlayOneShot(deathSound);
                Kill();
            }
        }

        #region ANIMATIONS
        private void SpawnAnimation()
        {
            transform.DOScale(0, spawnAnimationDuration).SetEase(spawnAnimationEase).From();
        }

        public void PlayAnimationByTrigger(AnimationType animationType)
        {
            _animationBase.PlayAnimationByTrigger(animationType);
        }

        #endregion

        // debug
        public virtual void Update()
        {
            LookAtPlayer();
        }

        public void Damage(int damage)
        {
            OnDamage(damage);
        }

        public void Damage(int damage, Vector3 direction)
        {
            OnDamage(damage);
            transform.DOMove(transform.position - direction, .1f);
        }

        private void OnCollisionEnter(Collision collision)
        {
            PlayerBase player = collision.transform.GetComponent<PlayerBase>();

            if (player != null)
            {
                player.health.Damage(damage);
            }
        }

        public void LookAtPlayer()
        {
            if (lookAtPlayer)
            {
                transform.LookAt(_player.transform.position);
            }
        }
    }
}
