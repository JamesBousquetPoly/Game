using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    // Start is called before the first frame update

    public float movementSpeed = 3.0f;
    Vector2 movement = new Vector2();
    Rigidbody2D rb2D;
    Animator animator;
    string animationState = "AnimationState";

    

    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();
    }

    private void FixedUpdate()
    {
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement.Normalize();
        rb2D.velocity = movement * movementSpeed;
    }

    private void UpdateState()
    {
        if(movement.x > 0)
        {
            animator.SetInteger(animationState, (int)CharStates.walkEast);
        } else if(movement.x < 0)
        {
            animator.SetInteger(animationState, (int)CharStates.walkWest);
        } else if (movement.y > 0)
        {
            animator.SetInteger(animationState, (int)CharStates.walkNorth);
        } else if (movement.y < 0)
        {
            animator.SetInteger(animationState, (int)CharStates.walkSouth);
        } else
        {
            animator.SetInteger(animationState, (int)CharStates.idleSouth);
        }
    }

    enum CharStates
    {
        walkEast = 1,
        walkWest,
        walkSouth,
        walkNorth,

        idleSouth
    }


}
