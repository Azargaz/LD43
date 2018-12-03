using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class MovementController : MonoBehaviour
{
	public float maxJumpHeight = 4;
    public float minJumpHeight = 1;
    public float jumpHeight = 4;
    public float timeToJumpApex = .4f;
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    public float moveSpeed = 6;

    float gravity;
	float maxJumpVelocity;
    float minJumpVelocity;
    Vector3 velocity;
    float velocityXSmoothing;

    Controller2D controller;
    Animator anim;
    Animator numberAnim;
    SpriteRenderer sprite;

    public bool stop = false;
    void Start()
    {
        controller = GetComponent<Controller2D>();
        anim = GetComponent<Animator>();
        numberAnim = transform.Find("Number").GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
		maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt (2 * Mathf.Abs (gravity) * minJumpHeight);  
        
        numberAnim.SetBool("hide", true); 
    }

    void Update()
    {
        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if(stop)
            input = Vector2.zero;

        if(!stop)
        {
            if(Input.GetButtonDown("Jump"))
            {            
                if (controller.collisions.below)
                {
                    anim.SetTrigger("jump");
                    velocity.y = maxJumpVelocity;
                }
            }

            if (Input.GetButtonUp("Jump"))
            {
                if (velocity.y > minJumpVelocity) 
                {
                    velocity.y = minJumpVelocity;
                }

            }
        }

        // anim.SetBool("pushing", Input.GetButton("Fire1"));
        anim.SetFloat("walk", Mathf.Abs(input.x));
        anim.SetFloat("Yvelocity", velocity.y);
        anim.SetBool("grounded", controller.collisions.below);

        sprite.flipX = input.x == 0 ? sprite.flipX : input.x < 0;

        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if(Input.GetButtonDown("Unstop"))
        {
            stop = false;
        }

        if(Input.GetButtonDown("Stop"))
        {
            stop = true;
        }

        if(Input.GetKeyDown(KeyCode.LeftAlt))
        {            
            numberAnim.SetBool("hide", !numberAnim.GetBool("hide"));
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {    
		if(other.gameObject.layer == 12)
		{
            MoveableBlockController block = other.gameObject.GetComponent<MoveableBlockController>();
            bool pushing = Input.GetAxisRaw("Horizontal") == block.input.x;
			anim.SetBool("pushing", pushing);
		}
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.layer == 12)
		{
			anim.SetBool("pushing", false);
		}
    }

    void OnMouseDown()
    {
        stop = !stop;
    }

    public void Stop()
    {
        stop = true;
        numberAnim.SetBool("hide", false);
    }

    public void StopUnstop()
    {
        stop = !stop;
        numberAnim.SetBool("hide", !stop);
    }
}
