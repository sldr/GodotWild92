using Godot;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.Intrinsics.Wasm;

public partial class Game : Node3D
{
    [Export] public double SpawnDespawnRadius = 50f;
    [Export] public int InitialSpawnCount = 25;
    [Export] public Player player;
    [Export] public PackedScene ZombieType1;
    [Export] public PackedScene ZombieType2;

    public void ZombieHit(Zombie zombie)
    {
        // TODO: Handle zombie hit
        GD.Print("Zombie HIT!");
        zombie.ZombieHitHandler();
    }

    public void ZombieSpawner(bool preGameStart = false)
    {
        MultiMeshInstance3D aaa;
        if (preGameStart) {
            GD.Print("PreGameStart");
            for (int i=0; i< InitialSpawnCount; i++) {
                float angle = (float)GD.RandRange(0, Mathf.Tau); // Random angle (0 to 360 degrees in radians)
                float t = (float)GD.Randf();
                float dist = (float)Mathf.Lerp(10d, SpawnDespawnRadius, Mathf.Sqrt(t)); // Random distance between 10 n SpawnDespawnRadius
                //float dist = (float)GD.RandRange(10d, SpawnDespawnRadius); // Random distance between 10 n SpawnDespawnRadius
                // Convert polar → Cartesian (XZ plane)
                Vector3 offset = new Vector3(
                    Mathf.Cos(angle),
                    0,
                    Mathf.Sin(angle)
                ) * dist + Vector3.Up * 2;
                Vector3 spawnPosition = GlobalPosition + offset;
                Zombie z = ZombieType1.Instantiate<Zombie>();
                AddChild(z);
                z.GlobalPosition = spawnPosition;
            }
        }

    }

    public override void _Process(double delta)
    {
        var zombies = GetTree().GetNodesInGroup("zombies");
        foreach (Node node in zombies) {
            if (node is Zombie zombie) {
                if (zombie != null && player != null) {
                    float distance = zombie.GlobalPosition.DistanceTo(player.GlobalPosition);
                    if (distance >= SpawnDespawnRadius) {
                        zombie.QueueFree();
                    }
                }
            }
        }
    }

    public override void _Ready()
    {
        GD.Print("Spawner");
        ZombieSpawner(true);
        GD.Print("Spawner finished");
    }


}
