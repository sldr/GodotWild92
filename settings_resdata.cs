using Godot;
using System;

public partial class settings_resdata : Resource
{
    [ExportCategory("Settings Saved Data")]
    [Export]
    public double pause_smoothing = 0;
    [ExportCategory("Settings Default Data")]
    [Export]
    public double pause_smoothing_default = 0;

}
