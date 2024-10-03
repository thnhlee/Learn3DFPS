using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("ThinhLe/PlayerController")]

public class PlayerController : MonoBehaviour
{
    [Header("Speed Character")]
    public float speed = 12f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;
    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistance = 0.4f;
    private Vector3 velocity;

    private CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isCheckGround() && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        //Movement
        float x = Input.GetAxis("Horizontal");
        float z= Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
        if (Input.GetButtonDown("Jump") && isCheckGround())
        {
            velocity.y = Mathf.Sqrt(jumpHeight * gravity * -2f);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    bool isCheckGround()
    {
        bool isGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        return isGround;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    }
}

