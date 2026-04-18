using Godot;
using System;

public partial class Bullet : RigidBody3D
{
	[Export] public float speed;
	[Export] public float maxDistance;
	[Export] public CharacterBody3D PlayerRef;

	private Vector3 velocity;
	private Vector3 startPosition;
	private RayCast3D bulletRay;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		//bulletRay = GetNode<RayCast3D>("Area3D/RayCast3D");
		//bulletRay.AddException(PlayerRef);
		//startPosition = GlobalPosition;
		//bulletRay.TargetPosition = new Vector3(0, 0, -maxDistance);


    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//GD.Print(bulletRay.TargetPosition);
		//if (bulletRay.IsColliding())
		
		
	}

    public override void _PhysicsProcess(double delta)
    {
		Vector3 A = Vector3.Zero;
		A.Z = -1 * speed;
		this.MoveAndCollide(A);
		var cols = this.GetCollidingBodies();
		foreach (Node3D N in cols)
		{
			if (N is Zombie Z)
			{
				Z.
			}
		}
    }

}
