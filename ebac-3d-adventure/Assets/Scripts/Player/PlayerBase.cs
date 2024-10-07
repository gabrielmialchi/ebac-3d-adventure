using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour//, IDamageable
{
    //references
    public CharacterController characterController;
    public Animator animator;
    public HealthBase health;

    //publics
    public float speed = 1f;
    public float turnSpeed = 1f;
    public float gravity = -9.8f;
    public float jumpSpeed = 15f;
    public float timeToRevive = 3f;

    public KeyCode jumpKeyCode = KeyCode.Space;

    [Header("Run Setup")]
    public KeyCode runKeyCode = KeyCode.LeftShift;
    public float runSpeed = 1.5f;

    [Header("Damage")]
    public List<FlashColor> flashColors;
    public List<Collider> colliders;


    //privates
    private float vSpeed = 0f;
    private bool _isAlive = true;

    private void OnValidate()
    {
        if (health == null) health = GetComponent<HealthBase>();
    }

    private void Awake()
    {
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
            vSpeed = 0;
            if (Input.GetKeyDown(jumpKeyCode))
            {
                vSpeed = jumpSpeed;
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
        flashColors.ForEach(i => i.Flash());
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
}