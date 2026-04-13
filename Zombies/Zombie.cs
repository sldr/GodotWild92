using Godot;
using System;

public partial class Zombie : RigidBody3D
{
    [Export(PropertyHint.Range, "1,2")]
    public int ZombieType = 1;

    public void _on_body_entered(Node body)
    {
        if (body.IsInGroup("player")) {
            this.QueueFree();
        }
    }

    public override void _Ready()
    {
        //this.BodyEntered += Zombie_BodyEntered;
    }

    private void Zombie_BodyEntered(Node body)
    {
        if (body.IsInGroup("player")) {
            this.QueueFree();
        }
    }
}
