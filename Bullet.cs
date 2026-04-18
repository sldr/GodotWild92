using Godot;
using System;

public partial class Bullet : RigidBody3D
{
	[Export] public float speed;
	[Export] public float maxDistance;
	[Export] public CharacterBody3D PlayerRef;

	public Vector3 direction;

	private Vector3 velocity;
	private Vector3 startPosition;
	private RayCast3D bulletRay;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		//bulletRay = GetNode<RayCast3D>("Area3D/RayCast3D");
		//bulletRay.AddException(PlayerRef);
		startPosition = Position;
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
		if (Position.DistanceTo(this.startPosition) >= maxDistance)
			this.QueueFree();
        Game game = GetTree().CurrentScene as Game;
		//GD.Print("Before direction: ", direction);
		//direction = new Vector3 (direction.X, 0, direction.Z);
        Vector3 A = direction * speed;
        //A.Y = 1;
        //GD.Print("After direction: ", direction);
        this.MoveAndCollide(A);
		var cols = this.GetCollidingBodies();
		if(cols != null)
			foreach (Node3D N in cols)
			{
				if (N is Zombie Z)
				{
					game.ZombieHit(Z);
					GD.Print("Hit!");
					this.QueueFree();
				}
			}
    }

}
