using Fusion;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour

{

    [SerializeField]
    CharacterController characterController;
    public float playerSpeed=5f;
    public float jumpforce=5f;

    float Gravity = -8.91f;

    Vector3 velocity;

    bool jumping;


    void Start()
    {
        characterController.GetComponent<CharacterController>();
    }
    private void Update()
    {


        if (Input.GetKeyDown(KeyCode.Space))
        {

            jumping = true;

        }

    }

    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();

        if (HasStateAuthority == false)
        {

            return;
        }


        if (characterController.isGrounded == true)
        {

            velocity = new Vector3(0, -1, 0);

        }
        else
        {

            jumping = false;
        }
        velocity.y += Gravity * Runner.DeltaTime;

        if (jumping && characterController.isGrounded)
        {

            velocity.y += jumpforce;

        }


        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");


        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput) * playerSpeed * Runner.DeltaTime;


        characterController.Move(movement + velocity * Runner.DeltaTime);
        if (movement != Vector3.zero)
        {

            gameObject.transform.forward = movement;
        }


    }


}
