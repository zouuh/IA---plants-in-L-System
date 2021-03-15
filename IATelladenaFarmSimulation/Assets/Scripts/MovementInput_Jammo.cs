
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script requires you to have setup your animator with 3 parameters, "InputMagnitude", "InputX", "InputZ"
//With a blend tree to control the inputmagnitude and allow blending between animations.
[RequireComponent(typeof(CharacterController))]
public class MovementInput : MonoBehaviour {

    public float Velocity;
    [Space]

	public float InputX;
	public float InputZ;
	public Vector3 desiredMoveDirection;
	public bool blockRotationPlayer;
	public float desiredRotationSpeed = 0.1f;
	public Animator anim;
	public float Speed;
	public float allowPlayerRotation = 0.1f;
	public Camera cam;
	public CharacterController controller;
	public bool isGrounded;

    [Header("Animation Smoothing")]
    [Range(0, 1f)]
    public float HorizontalAnimSmoothTime = 0.2f;
    [Range(0, 1f)]
    public float VerticalAnimTime = 0.2f;
    [Range(0,1f)]
    public float StartAnimTime = 0.3f;
    [Range(0, 1f)]
    public float StopAnimTime = 0.15f;

    public float verticalVel;
    private Vector3 moveVector;

	public int nrOfAlowedDJumps = 0;
	int dJumpCounter = 0;
	public float jumpSpeed = 20;

	Vector3 teleportation = Vector3.zero;
	bool isTeleporting = false;

	// Use this for initialization
	void Start () {
		anim = this.GetComponent<Animator> ();
		cam = Camera.main;
		controller = this.GetComponent<CharacterController> ();
		verticalVel = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
        // add input to change Velocity
        if (Input.GetKeyDown(KeyCode.C))
        {
			Velocity = 2;
			//anim.SetFloat("Blend", 0.1f);
		}
        else if(Input.GetKeyUp(KeyCode.C))
        {
			Velocity = 10;
			//anim.SetFloat("Blend", 0.6f);
		}

		InputMagnitude();

		isGrounded = controller.isGrounded;
		
		if (Input.GetButtonDown("Jump"))
		{
			if (isGrounded)
			{
				verticalVel = jumpSpeed;
				dJumpCounter = 0;
			}
			if (!isGrounded && dJumpCounter < nrOfAlowedDJumps)
			{
				verticalVel = jumpSpeed;
				dJumpCounter++;
			}
		}
		
		if (!isGrounded)
		{
            verticalVel -= .5f;
		}
		

		if(isTeleporting)
		{
			UnityEngine.Debug.Log(teleportation);
			moveVector = teleportation;
			isTeleporting = false;
        }
        else
        {
			moveVector = new Vector3(0, verticalVel * .5f * Time.deltaTime, 0);
		}
		
		//moveVector = new Vector3(0, verticalVel * .2f * Time.deltaTime, 0);
        controller.Move(moveVector);
		

    }

	public void Climb(Vector3 newPos)
	{
		UnityEngine.Debug.Log("climb!");
		//controller = this.GetComponent<CharacterController>();
		//controller.enabled = true;
		//controller.Move(newPos);

		//transform.position = newPos;
		//controller.enabled = false;
		//this.transform.position = newPos;
		//controller.enabled = true;
		//verticalVel = 30;
		//controller.enabled = true;
		//controller.gameObject.SetActive(true);
		//controller.Move(Vector3.forward);
		/*
		controller.enabled = false;
		transform.Translate(5,5,5);
		controller.enabled = true;
		*/
		teleportation = newPos;
		isTeleporting = true;
		//UnityEngine.Debug.Log(isTeleporting);
	}

	void PlayerMoveAndRotation() {
		InputX = Input.GetAxis ("Horizontal");
		InputZ = Input.GetAxis ("Vertical");

		var camera = Camera.main;
		var forward = cam.transform.forward;
		var right = cam.transform.right;

		forward.y = 0f;
		right.y = 0f;

		forward.Normalize ();
		right.Normalize ();

		desiredMoveDirection = forward * InputZ + right * InputX;

		if (blockRotationPlayer == false) {
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (desiredMoveDirection), desiredRotationSpeed);
            controller.Move(desiredMoveDirection * Time.deltaTime * Velocity);
		}
	}

    public void LookAt(Vector3 pos)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(pos), desiredRotationSpeed);
    }

    public void RotateToCamera(Transform t)
    {

        var camera = Camera.main;
        var forward = cam.transform.forward;
        var right = cam.transform.right;

        desiredMoveDirection = forward;

        t.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
    }

	void InputMagnitude() {
		//Calculate Input Vectors
		InputX = Input.GetAxis ("Horizontal");
		InputZ = Input.GetAxis ("Vertical");

		//anim.SetFloat ("InputZ", InputZ, VerticalAnimTime, Time.deltaTime * 2f);
		//anim.SetFloat ("InputX", InputX, HorizontalAnimSmoothTime, Time.deltaTime * 2f);

		//Calculate the Input Magnitude
		Speed = new Vector2(InputX, InputZ).sqrMagnitude;

        //Physically move player
        if (Speed > allowPlayerRotation && Velocity <= 3)
        {
			anim.SetFloat("Blend", 0.2f, StartAnimTime, Time.deltaTime);
			PlayerMoveAndRotation();
        }else if (Speed > allowPlayerRotation) {
			anim.SetFloat ("Blend", Speed, StartAnimTime, Time.deltaTime);
			PlayerMoveAndRotation ();
		} else if (Speed < allowPlayerRotation) {
			anim.SetFloat ("Blend", Speed, StopAnimTime, Time.deltaTime);
		}
	}
}
