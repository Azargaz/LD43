using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class MoveableBlockController : MonoBehaviour 
{
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    public float moveSpeed = 6;

    public float gravity = -6;
    Vector3 velocity;
    float velocityXSmoothing;

    Controller2D controller;

	public float movingRange = 0.3f;
	public Vector2 input;

    void Start()
    {
        controller = GetComponent<Controller2D>();
    }

    void Update()
    {
        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

		if(!Input.GetButton("Fire1"))
			StopX();
		
        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

	void MoveAlongX(bool forward)
	{
		input.x = forward ? 1 : -1;
	}

	void StopX()
	{
		input.x = 0;
	}

	void OnCollisionStay2D(Collision2D other) 
	{
		if(other.gameObject.layer == 8 && Input.GetButton("Fire1"))
		{
			Vector2 colliderRange = GetComponent<Collider2D>().bounds.extents;
			Vector2 distance = other.transform.position - transform.position;
			
			if(Mathf.Abs(distance.y) < colliderRange.y && Mathf.Abs(distance.x) < colliderRange.x + movingRange)
			{
				bool forward = distance.x < 0;
				MoveAlongX(forward);
			}
		}
	}

	void OnCollisionExit2D(Collision2D other) 
	{
		if(other.gameObject.layer == 8)
		{
			StopX();
		}
	}
}
