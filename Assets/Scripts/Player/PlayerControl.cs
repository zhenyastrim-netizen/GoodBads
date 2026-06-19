using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public float MoveSpeed=3;
    public SpriteRenderer sr;
    public InputAction MoveAction;
    public Animator animator;
    public InputAction FireAction;
    private Vector2 lastMoveDirection = Vector2.down;
   
    
    Rigidbody2D rb;
    
   
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FireAction.Enable();
        animator=GetComponent<Animator>();
        sr=GetComponent<SpriteRenderer>();
        MoveAction.Enable();
        rb=GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        Vector2 move=MoveAction.ReadValue<Vector2>();
        Vector2 position=(Vector2)transform.position+move*MoveSpeed*Time.deltaTime;
        rb.MovePosition(position);
       

        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        

        

        if (move.sqrMagnitude > 0.01f)
        {
        lastMoveDirection = move.normalized;

        animator.SetFloat("Horizontal", lastMoveDirection.x);
        animator.SetFloat("Vertical", lastMoveDirection.y);
        }

        animator.SetFloat("MoveSpeed", move.sqrMagnitude);
       /*if (mousePos.x<transform.position.x)
        {
            sr.flipX=true;
        }
        else
        {
            sr.flipX=false;
        }
        */
    }


    }


