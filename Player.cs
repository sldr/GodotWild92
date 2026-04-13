using Godot;
using System;

public partial class Player : CharacterBody3D
{
	//Globals
	[Export] public float Speed = 5.0f;
    [Export] public float TurnSpeed = 4.0f;
    [Export] public float JumpVelocity = 2.5f;
	public Timer ImpulseTimer;
	private Vector3 impulseAmount = Vector3.Zero;


    //Flags
    bool isSpeedBoostActive = false;

    public override void _Ready()
    {
		ImpulseTimer = GetNode<Timer>("ImpulseTimer");
    }

    public override void _PhysicsProcess(double delta)
	{
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
		RotateY(turnInput * TurnSpeed * (float)delta);

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