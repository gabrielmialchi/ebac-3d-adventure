using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour, IDamageable
{
    public CharacterController characterController;
    public Animator animator;
    public HealthBase health;

    public float speed = 1f;
    public float turnSpeed = 1f;
    public float gravity = -9.8f;
    public float jumpSpeed = 15f;

    public KeyCode jumpKeyCode = KeyCode.Space;

    private float vSpeed = 0f;

    [Header("Run Setup")]
    public KeyCode runKeyCode = KeyCode.LeftShift;
    public float runSpeed = 1.5f;

    [Header("Damage")]
    public List<FlashColor> flashColors;

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
        //if (flashColors != null) flashColors.Flash();
        //if (damageParticleSystem != null) damageParticleSystem.Play();

        //transform.position -= transform.forward;
        health._currentLife -= damage;

        if (health._currentLife <= 0)
        {
            health.Kill();
        }
    }

    public void Damage(int damage)
    {
        flashColors.ForEach(i => i.Flash());
    }

    public void Damage(int damage, Vector3 direction)
    {
        Damage(damage);
    }
    #endregion
}