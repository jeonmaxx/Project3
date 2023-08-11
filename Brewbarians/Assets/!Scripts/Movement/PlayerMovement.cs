using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 m_PlayerMovement;
    private Rigidbody2D rb;
    [SerializeField]
    private float speed = 5;
    [HideInInspector] public Animator animator;
    DialogueManager dialogueManager;
    public InputActionReference actionRef;
    private InputAction m_MoveAction;

    private void Awake()
    {
        m_MoveAction = actionRef.action;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    public void Update()
    {
        if (dialogueManager.isActive == false)
        {
            m_PlayerMovement = m_MoveAction.ReadValue<Vector2>();
        }
        else
        {
            m_PlayerMovement = new Vector2(0, 0);
        }

        if (m_PlayerMovement.x != 0 || m_PlayerMovement.y != 0)
        {
            animator.SetFloat("X", m_PlayerMovement.x);
            animator.SetFloat("Y", m_PlayerMovement.y);

            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + m_PlayerMovement * speed * Time.fixedDeltaTime);
    }
}
