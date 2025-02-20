using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Player
{

    [RequireComponent(typeof(Rigidbody))]

    public class PlayerMovement : MonoBehaviour
    {
        public float speed;
        public float mouseSensivity;
        public float jumpForce;
        private Rigidbody rb;
        private Camera mainCamera;
        private float yaw = 0f;
        private float pitch = 0f;
        private bool isGrounded = false;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            mainCamera = Camera.main;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void FixedUpdate()
        {
            CameraRotate();
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                PlayerJump();
            }
            PlayerMove();
        }

        private void CameraRotate()
        {
            float mouseX = Input.GetAxisRaw("Mouse X");
            float mouseY = Input.GetAxisRaw("Mouse Y");

            yaw += mouseX * mouseSensivity;
            pitch -= mouseY * mouseSensivity;

            pitch = Mathf.Clamp(pitch, -80f, 80f);

            mainCamera.transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
            transform.rotation = Quaternion.Euler(0f, yaw, 0f);
        }
        private void PlayerMove()
        {
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveZ = Input.GetAxisRaw("Vertical");

            Vector3 forward = mainCamera.transform.forward;
            Vector3 right = mainCamera.transform.right;

            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();

            Vector3 direction = (forward * moveZ + right * moveX).normalized;

            rb.velocity = new Vector3(direction.x * speed, rb.velocity.y, direction.z * speed);
        }

        private void PlayerJump()
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        private void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                isGrounded = true;
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                isGrounded = false;
            }
        }
    }

}
