using UnityEngine;
using Ebac.Core.Singleton;
using System.Collections.Generic;
using System.Collections;
using Cloth;

public class PlayerBase : Singleton<PlayerBase> //, IDamageable
{
    //references
    public CharacterController characterController;
    public Animator animator;
    public HealthBase health;
    public AudioSource damageAS;

    //publics
    public float speed = 1f;
    public float turnSpeed = 1f;
    public float gravity = -9.8f;
    public float jumpSpeed = 15f;
    public float timeToRevive = 3f;

    [Header("Controls")]
    public KeyCode jumpKeyCode = KeyCode.Space;
    public KeyCode runKeyCode = KeyCode.LeftShift;

    [Space]
    [Header("Run Setup")]
    public float runSpeed = 1.5f;

    [Space]
    [Header("Damage")]
    public List<FlashColor> flashColors;
    public List<Collider> colliders;

    [Space]
    [Header("Clothes")]
    [SerializeField] private ClothChanger _clothChanger;

    [Space]
    [Header("Sound")]
    public AudioClip damageSound;
    
    //privates
    private float vSpeed = 0f;
    private bool _isAlive = true;
    private bool _isJumping = true;

    private void OnValidate()
    {
        if (health == null) health = GetComponent<HealthBase>();
    }

    protected override void Awake()
    {
        base.Awake();
        OnValidate();

        health.OnDamageAction += Damage;
        health.OnKillAction += OnKill;
    }

    private void Update()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);

        var inputAxisVertical = Input.GetAxis("Vertical");
        var speedVector = transform.forward * inputAxisVertical * speed;

        if (characterController.isGrounded)
        {
            if (_isJumping)
            {
                _isJumping = false;
                animator.SetTrigger("Land");
            }
            vSpeed = 0;

            if (Input.GetKeyDown(jumpKeyCode))
            {
                vSpeed = jumpSpeed;
                if (!_isJumping)
                {
                    _isJumping = true;
                    animator.SetTrigger("Jump");
                }
            }
        }

        vSpeed -= gravity * Time.deltaTime;
        speedVector.y = vSpeed;

        var isWalking = inputAxisVertical != 0;

        if (isWalking)
        {
            if (Input.GetKey(runKeyCode))
            {
                speedVector *= runSpeed;
                animator.speed = runSpeed;
            }
            else
            {
                animator.speed = 1;
            }
        }

        characterController.Move(speedVector * Time.deltaTime);

        animator.SetBool("Run", inputAxisVertical != 0);
    }


    #region LIFE
    public void OnDamage(int damage)
    {
        health._currentLife -= damage;

        if (health._currentLife <= 0)
        {
            health.Kill();
        }
    }

    public void Damage(HealthBase h)
    {
        //damageAS.PlayOneShot(damageSound);
        flashColors.ForEach(i => i.Flash());
        FXManager.Instance.ChangeVignette();
    }

    public void Damage(int damage, Vector3 direction)
    {
        //Damage(damage);
    }

    private void OnKill(HealthBase h)
    {
        if (_isAlive)
        {
            _isAlive = false;
            animator.SetTrigger("Death");
            colliders.ForEach(i => i.enabled = false);

            Invoke(nameof(Revive), timeToRevive);
        }
    }

    private void Revive()
    {
        _isAlive = true;
        health.ResetLife();
        animator.SetTrigger("Revive");
        Respawn();
        colliders.ForEach(i => i.enabled = true);
    }
    #endregion

    [NaughtyAttributes.Button]
    public void Respawn()
    {
        if (CheckpointManager.Instance.HasCheckpoint())
        {
            transform.position = CheckpointManager.Instance.GetLastCheckpointRespawnPosition();
        }
    }

    public void ChangeSpeed(float speed, float duration)
    {
        StartCoroutine(ChangeSpeedCoroutine(speed, duration));
    }

    IEnumerator ChangeSpeedCoroutine(float localSpeed, float duration)
    {
        var defaultSpeed = speed;
        speed = localSpeed;
        yield return new WaitForSeconds(duration);
        speed = defaultSpeed;
    }

    public void ChangeTexture(ClothSetup setup, float duration)
    {
        StartCoroutine(ChangeTextureCoroutine(setup, duration));
    }

    IEnumerator ChangeTextureCoroutine(ClothSetup setup, float duration)
    {
        _clothChanger.ChangeTexture(setup);
        yield return new WaitForSeconds(duration);
        _clothChanger.ResetTexture();
    }
}