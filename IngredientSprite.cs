using Godot;
using System;

public partial class IngredientSprite : Node3D
{
	//Globals
	private AnimationPlayer hoverHandler;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        hoverHandler = this.GetNode<AnimationPlayer>("IngredientSprite/hoverHandler");
    }

	public void _on_ingredient_sprite_input_event(Node A, InputEvent B, Vector3 C, Vector3 D, int E)
	{
		if (B is InputEventMouseButton leftClick)
			if (leftClick.ButtonIndex == MouseButton.Left)
				if (leftClick.Pressed)
					GD.Print("KABLEWEEE");
	}

    public void _on_ingredient_sprite_mouse_entered()
	{
        hoverHandler.Play("whenHover");
    }

	public void _on_ingredient_sprite_mouse_exited()
	{
		hoverHandler.PlayBackwards("whenHover");
	}
}
