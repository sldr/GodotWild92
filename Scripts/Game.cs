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
    [Export] public MultiMeshInstance3D MultiMeshInstance;
    [Export] public PackedScene ZombieShell;
    private int updateZombieGrouping = 0;
    private MultiMesh mm;
    private int mmInstanceCount = 0;
    private Dictionary<int, Transform3D> mmZombiePositions = new Dictionary<int, Transform3D>();
    private Dictionary<int, Zombie> mmZombieShells = new Dictionary<int, Zombie>();
    private static int onetime = 0;

    public void ZombieHit(Zombie zombie)
    {
        if (zombie == null) {
            return;
        }
        //if (onetime++ <2) {
            // TODO: Handle zombie hit
            //GD.Print("Zombie HIT! ", zombie.Index);
            RemoveZombieInstance(zombie.Index);
            zombie.ZombieHitHandler();
        //}
    }

    public void ZombieSpawner(bool preGameStart = false)
    {
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

    public void ZombieSpawnerMultiMesh(MultiMesh mm, bool preGameStart = false)
    {
        if (preGameStart) {
            GD.Print("PreGameStart");
            for (int i = 0; i < mm.InstanceCount; i++) {
                float angle = (float)GD.RandRange(0, Mathf.Tau); // Random angle (0 to 360 degrees in radians)
                float t = (float)GD.Randf();
                float dist = (float)Mathf.Lerp(10d, SpawnDespawnRadius, Mathf.Sqrt(t)); // Random distance between 10 n SpawnDespawnRadius
                //float dist = (float)GD.RandRange(10d, SpawnDespawnRadius); // Random distance between 10 n SpawnDespawnRadius
                // Convert polar → Cartesian (XZ plane)
                Vector3 offset = new Vector3(
                    Mathf.Cos(angle),
                    0,
                    Mathf.Sin(angle)
                ) * dist + Vector3.Up;
                Vector3 spawnPosition = GlobalPosition + offset;
                var transform = Transform3D.Identity;
                transform.Origin = spawnPosition;
                mm.SetInstanceTransform(i, transform);
                mmZombiePositions.Add(i, transform);
                Zombie shell = ZombieShell.Instantiate<Zombie>();
                shell.Index = i;
                AddChild(shell);
                shell.Transform = transform;
                mmZombieShells.Add(i, shell);
            }
            this.mmInstanceCount = mm.InstanceCount;
        }
    }

    void RemoveZombieInstance(int index)
    {
        if (index == -1) { 
            return; // Not a MultiMesh Zombie
        }
        int last = mmInstanceCount - 1;
        if (index != last) {
            mm.SetInstanceTransform(index, mm.GetInstanceTransform(last));
            mmZombiePositions[index] = mmZombiePositions[last];
            mmZombieShells[index] = mmZombieShells[last];


            var t = mm.GetInstanceTransform(last);
            t.Basis = Basis.Identity.Scaled(Vector3.Zero);
            mm.SetInstanceTransform(index, t);
        }
        //mm.InstanceCount -= 1;
        mmInstanceCount--;
        mmZombieShells[last].QueueFree();
        mmZombiePositions.Remove(last);
        mmZombieShells.Remove(last);
        if (mmInstanceCount % 10 == 0) {
            GD.Print("Instances: ", mmInstanceCount);
        }
    }

    public override void _Process(double delta)
    {
        //var zombies = GetTree().GetNodesInGroup("zombies");
        //foreach (Node node in zombies) {
        //    if (node is Zombie zombie) {
        //        if (zombie != null && player != null) {
        //            float distance = zombie.GlobalPosition.DistanceTo(player.GlobalPosition);
        //            if (distance >= SpawnDespawnRadius) {
        //                RemoveZombieInstance(zombie.Index);
        //                zombie.QueueFree();
        //            }
        //        }
        //    }
        //}
        // Update the positions of 1/8 of the Zombies on each frame // fix adjust this
        //GD.Print("Doing group: ", updateZombieGrouping);
        for (int i = 0; i < mmInstanceCount; i++) {
            if (i % 8 == updateZombieGrouping) {
                try {
                    mmZombiePositions[i] = mmZombieShells[i].GlobalTransform;
                    mm.SetInstanceTransform(i, mmZombiePositions[i]);
                } catch {
                    // Ignore
                }
            }
        }
        updateZombieGrouping++;
        if (updateZombieGrouping >= 8) {
            updateZombieGrouping = 0;
        }
    }

    public override void _Ready()
    {
        GD.Print("Spawner");
        //ZombieSpawner(true);
        MultiMeshInstance.Visible = true;
        mm = MultiMeshInstance.Multimesh;
        ZombieSpawnerMultiMesh(MultiMeshInstance.Multimesh, true);
        GD.Print("Spawner finished");
    }


}
