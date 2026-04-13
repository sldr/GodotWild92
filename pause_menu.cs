using Godot;
using System;

public partial class pause_menu : CanvasLayer
{
    // Node refs
    AnimationPlayer PauseAnim;
    HSlider pSmoothing;

    // Local data
    private double pause_smoothing;
    private float pSmoothScale = 1;

    // Save data
    [Export]
    settings_resdata settingsResData;

    // Flags
    bool paused = false;
    public override void _Ready()
    {
        PauseAnim = this.GetNode<AnimationPlayer>("AnimationPlayer");
        pSmoothing = this.GetNode<HSlider>("MarginContainer/VBoxContainer/MarginContainer/VBoxContainer/HBoxContainer/HSlider");
        pSmoothing.Value = settingsResData.pause_smoothing;
        pSmoothScale = 40 * (float)pause_smoothing + 1;
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("pause") && paused == false)
        {
            pSmoothing.Value = settingsResData.pause_smoothing;
            this.Visible = true;
            PauseAnim.Play("smooth pause", -1, pSmoothScale);
            paused = true;
            GetTree().Paused = true;
        }
        else if (Input.IsActionJustPressed("pause") && paused == true)
        {
            resume();
        }
    }
    public void _on_h_slider_value_changed(float value)
    {
        pause_smoothing = value;
    }
    public void _on_resume_pressed()
    {
        resume();
    }
    private void resume()
    {
        this.Visible = false;
        paused = false;
        GetTree().Paused = false;
    }
    public void _on_save_pressed()
    {
        settingsResData.pause_smoothing = pause_smoothing;
        ResourceSaver.Save(settingsResData);
        pSmoothScale = 40 * (float)pause_smoothing + 1;
        pSmoothing.Value = pause_smoothing;
    }
    public void _on_reset_pressed()
    {
        pause_smoothing = settingsResData.pause_smoothing_default;
        settingsResData.pause_smoothing = pause_smoothing;
        ResourceSaver.Save(settingsResData);
        pSmoothScale = 40 * (float)pause_smoothing + 1;
        pSmoothing.Value = settingsResData.pause_smoothing_default;
    }
    public void _on_quit_pressed()
    {
        GetTree().Quit();
    }

}
