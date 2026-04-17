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
        GD.Print("IngredientSprite ready!");
    }

	public void _on_ingredient_sprite_mouse_entered()
	{
        GD.Print("MOUSE HOVERED");
        hoverHandler.Play("whenHover");
    }
}
