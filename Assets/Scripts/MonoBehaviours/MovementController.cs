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
    Weapon.Quadrant facing;

    

    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();
        if (Input.GetKeyDown("d"))
        {
            facing = Weapon.Quadrant.East;
        }
        else if (Input.GetKeyDown("a"))
        {
            facing = Weapon.Quadrant.West;
        }
        else if (Input.GetKeyDown("w"))
        {
            facing = Weapon.Quadrant.North;
        }
        else if(Input.GetKeyDown("s"))
        {
            facing = Weapon.Quadrant.South;
        }
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
        if(Mathf.Approximately(movement.x, 0) && Mathf.Approximately(movement.y, 0))
        {
            animator.SetBool("isWalking", false);
        } else
        {
            animator.SetBool("isWalking", true);
        }
        animator.SetFloat("xDir", movement.x);
        animator.SetFloat("yDir", movement.y);
    }

    public Weapon.Quadrant GetFacing()
    {
        return facing;
    }



}
