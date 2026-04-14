using Godot;
using System;

public partial class Zombie : RigidBody3D
{
    [Export(PropertyHint.Range, "1,2")]
    public int ZombieType = 1;

    public override void _Ready()
    {
        //this.BodyEntered += Zombie_BodyEntered;
    }

    public void ZombieHitHandler()
    {
        this.QueueFree();
    }
}
