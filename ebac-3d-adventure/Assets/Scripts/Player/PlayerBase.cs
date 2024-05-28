using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public CharacterController characterController;
    public Animator animator;

    public float speed = 1f;
    public float turnSpeed = 1f;
    public float gravity = -9.8f;
    public float jumpSpeed = 15f;

    public KeyCode jumpKeyCode = KeyCode.Space;

    private float vSpeed = 0f;

    [Header("Run Setup")]
    public KeyCode runKeyCode = KeyCode.LeftShift;
    public float runSpeed = 1.5f;

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
}