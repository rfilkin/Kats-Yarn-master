using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CentralGravity))]
public class PlayerJump : MonoBehaviour {

	[SerializeField] bool useAdvancedFormula = false;
	[SerializeField] float maxJumpHeight = 1f;
	[SerializeField] float minJumpHeight;
	[SerializeField] float timeToMaxJump = 1.5f;
	[SerializeField] float groundDetectionRadius;
	[SerializeField] float jumpCooldown = 0.1f;
	[SerializeField] Transform footPoint;
	[SerializeField] LayerMask groundLayers;
	[SerializeField] PlayerSoundBank playerSounds = null;

	KeyCode jump;
	Rigidbody2D body;
	[HideInInspector] public CentralGravity gravity;

	public bool isGrounded;
	bool jumpNextFrame = false;
	bool stillJumping = false;
	float cooldown = 0f;
	float timer = 0f;
	int jumpCount = 0;

	void Start () {
		jump = PlayerControlMap.jump;
		body = GetComponent<Rigidbody2D>();
		gravity = GetComponent<CentralGravity>();
	}

	//called every frame
	void Update(){
		UpdateGrounding();
		Cooldown();
		JumpInput();
	}

	//called on physics frames
	void FixedUpdate () {
		UpdateGrounding();
		if(useAdvancedFormula){
			NewJumpForce();
		} else
			JumpForce();
	}

	public int GetJumpCount(){
		return jumpCount;
	}

	//diminishes the cooldown on the jump
	void Cooldown(){
		cooldown -= Time.deltaTime;
		cooldown = Mathf.Clamp(cooldown, 0, jumpCooldown);
	}

	//if the player has pressed the jump key, is standing on the ground, and isn't spamming, then do a jump on the next physics frame
	void JumpInput(){
		if (Input.GetKeyDown(jump) && isGrounded && cooldown <= 0 && Statics.PlayerHasControl){
			jumpNextFrame = true;
			stillJumping = true;
		}
		if (Input.GetKeyUp(jump)){
			stillJumping = false;
		}
	}
		
	//applies the jump force to the player, and adapts if the player's gravity is inverted
	void JumpForce(){
		if (!jumpNextFrame)
			return;
		jumpNextFrame = false;
		++jumpCount;
		JumpSound();
		float magnitude = Mathf.Sqrt(2 * CentralGravity.GetGravityAcceleration() * minJumpHeight);
		if(gravity.reversed)
			body.AddForce(transform.up * -magnitude * body.mass, ForceMode2D.Impulse);
		else
			body.AddForce(transform.up * magnitude * body.mass, ForceMode2D.Impulse);
	}

	//applies jump force to the player
	void NewJumpForce(){
		float magnitude = 0;
		if (jumpNextFrame){
			magnitude = Mathf.Sqrt(2 * CentralGravity.GetGravityAcceleration() * minJumpHeight);
			jumpNextFrame = false;
			++jumpCount;
		} else if(stillJumping && timer < timeToMaxJump){
			float dif = maxJumpHeight - minJumpHeight;
			if (dif <= 0)
				return;
			magnitude = ContinuousVelocity() * Time.fixedDeltaTime;
			timer += Time.fixedDeltaTime;
		}
		ApplyForce(magnitude);
	}

	//calculates the velocity increase needed to reach the target height in the given time
	float ContinuousVelocity(){
		float heightNeeded = maxJumpHeight - minJumpHeight;
		float gravityResistance = timeToMaxJump * CentralGravity.GetGravityAcceleration() / 2;
		return heightNeeded + gravityResistance;
	}

	void ApplyForce(float magnitude){
		if (gravity.reversed)
			magnitude = -magnitude;
		body.AddForce(transform.up * -magnitude * body.mass, ForceMode2D.Impulse);
	}

	/*
	void OnCollisionStay2D(Collision2D groundcollider){
		isGrounded = true;
	}
	
	void OnCollisionExit2D(Collision2D groundcollider){
		isGrounded = false;
	}
	*/

	//checks if the player is grounded and assigns the value to 'isGrounded'
	void UpdateGrounding(){
		bool groundedThisFrame = CheckIsGrounded();
		if (groundedThisFrame && !isGrounded)
			LandSound();
		isGrounded = CheckIsGrounded();
	}

	//checks if the player is grounded
	bool CheckIsGrounded(){
		Collider2D hit = Physics2D.OverlapCircle(footPoint.position, groundDetectionRadius, groundLayers);
		return hit != null;
	}

	void LandSound(){
		if (playerSounds != null)
			playerSounds.OnLand();
	}

	void JumpSound(){
		if (playerSounds != null)
			playerSounds.OnJump();
	}

}
