using Godot;
using System;

public partial class pause_menu : CanvasLayer
{
    // Local
    AnimationPlayer PauseAnim;
    // Flags
    bool paused = false;
    public override void _Ready()
    {
        PauseAnim = this.GetNode<AnimationPlayer>("AnimationPlayer");
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("pause") && paused == false)
        {
            this.Visible = true;
            PauseAnim.Play("smooth pause");
            paused = true;
            GetTree().Paused = true;
        }
        else if (Input.IsActionJustPressed("pause") && paused == true)
        {
            this.Visible = false;
            paused = false;
            GetTree().Paused = false;
        }
    }

    public void _on_quit_pressed()
    {
        GetTree().Quit();
    }

}
