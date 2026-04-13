using Godot;
using System;

public partial class GameStats : Node
{

    public int Kills;

    public void ResetStats()
    {
        Kills = 0;
    }
}
