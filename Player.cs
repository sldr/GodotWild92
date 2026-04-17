using Godot;
using System;
using System.Reflection.Metadata.Ecma335;

public partial class Player : CharacterBody3D
{
	//Globals
	[Export] public float Speed = 5.0f;
    [Export] public float MaxTurnSpeed = 2.0f;
	[Export] public float TurnAcceleration = 2f;
	[Export] public float TurnDeacceleration = 4f;
    [Export] public float JumpVelocity = 2.5f;
	public Timer ImpulseTimer;
	private Vector3 impulseAmount = Vector3.Zero;
    private Game game = null;
	private float currentTurnSpeed = 0f;
	private AnimationPlayer povHandler;

    //Flags
    bool isSpeedBoostActive = false;
	bool isBrewing = false;
    int cameraPosition = 0;

    public override void _Ready()
    {
		ImpulseTimer = GetNode<Timer>("ImpulseTimer");
		povHandler = GetNode<AnimationPlayer>("CameraContainer/Camera3D/POVAnimPlayer");
    }

    public override void _PhysicsProcess(double delta)
	{
        if (game == null) {
            game = GetTree().CurrentScene as Game;
        }

        Vector3 velocity = Velocity;
        double currentTime = ImpulseTimer.TimeLeft;

        // Add the gravity.
        if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			velocity.Y += JumpVelocity;
        }

		// Enables turning left and right
		float turnInput = Input.GetAxis("turn_right", "turn_left");
		if (Input.IsActionPressed("turn_right") || Input.IsActionPressed("turn_left"))
		{
			currentTurnSpeed = Mathf.Lerp(currentTurnSpeed, MaxTurnSpeed, turnInput * TurnAcceleration * (float)delta);
		}
		else
		{
			currentTurnSpeed = Mathf.Lerp(currentTurnSpeed, 0, TurnDeacceleration * (float)delta);
		}

		currentTurnSpeed = Mathf.Clamp(currentTurnSpeed, -MaxTurnSpeed, MaxTurnSpeed);
		RotateY(currentTurnSpeed * (float)delta);

        // Constant forward movement
        Vector3 forward = -Transform.Basis.Z.Normalized();
			velocity.X = forward.X * Speed;
			velocity.Z = forward.Z * Speed;
        
		// Speed Boost Handler
        if (isSpeedBoostActive && !getImpulseStatus())
        {
            velocity += impulseAmount;
        }


        Velocity = velocity;
		MoveAndSlide();
        for (int i =0; i<this.GetSlideCollisionCount(); i++) {
            var collision = this.GetSlideCollision(i);
            if (collision.GetCollider() is Zombie zombie) {
                game.ZombieHit(zombie);
            }
        }
	}

    public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("brew"))
		{
			isBrewing = !isBrewing;

			if (isBrewing && cameraPosition == 0)
			{
                povHandler.Play("brew");
			}
			else if (!isBrewing && cameraPosition == 0)
			{
                povHandler.PlayBackwards("brew");
            }

            if (isBrewing && cameraPosition == 1)
            {
                povHandler.PlayBackwards("brewToPOV2");
            }
            else if (!isBrewing && cameraPosition == 1)
			{
                povHandler.Play("brewToPOV2");
			}

            if (isBrewing && cameraPosition == 2)
            {
                povHandler.PlayBackwards("brewToPOV3");
            }
            else if (!isBrewing && cameraPosition == 2)
            {
                povHandler.Play("brewToPOV3");
            }

            if (isBrewing && cameraPosition == 3)
            {
                povHandler.PlayBackwards("brewToPOV4");
            }
            else if (!isBrewing && cameraPosition == 3)
            {
                povHandler.Play("brewToPOV4");
            }

        }

		if (Input.IsActionJustPressed("changePOV"))
		{
            if (!isBrewing)
			{
				cameraPosition++;

                if (cameraPosition == 4)
                {
					cameraPosition = 0;
                    povHandler.Play("POV4ToPOV1");
                }
                else if (cameraPosition == 1)
                {
                    povHandler.Play("POV1ToPOV2");
                }
                else if (cameraPosition == 2)
                {
                    povHandler.Play("POV2ToPOV3");
                }
                else if (cameraPosition == 3)
                {
                    povHandler.Play("POV3ToPOV4");
                }
            }

			else
			{
				; /* Do nothing */
			}
        }
    }

	public void ApplyImpulse(Vector3 impulse, float duration)
	{
		isSpeedBoostActive = true;
        impulseAmount = impulse;
        ImpulseTimer.WaitTime = duration;
		ImpulseTimer.Start();
    }

    public void _on_impulse_timer_timeout()
    {
		isSpeedBoostActive = false;
		impulseAmount = Vector3.Zero;
        ImpulseTimer.Stop();
    }

	public bool getImpulseStatus()
	{
        return ImpulseTimer.IsStopped();
    }
}























/*
using Godot;
using System;

public partial class Player : CharacterBody3D
{
	public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;

	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 inputDir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Z = direction.Z * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
*/