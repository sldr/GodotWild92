using Godot;
using System;

public partial class Game : Node3D
{

    public void ZombieHit(Zombie zombie)
    {
        // TODO: Handle zombie hit
        GD.Print("Zombie HIT!");
        zombie.ZombieHitHandler();
    }

    public override void _Ready()
    {
        // TODO: Add stuff hear!
    }
}
