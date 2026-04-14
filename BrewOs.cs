using Godot;
using System;

public partial class BrewOs : Control
{
	// Node refs
	private Sprite2D gunpowderSprite;
	private Sprite2D rabitsfootSprite;
	private Sprite2D rocketfuelSprite;
	private Sprite2D gasolineSprite;

	// Local Data
	private int currentItemID = -1;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		gunpowderSprite = GetNode<Sprite2D>("MarginContainer/HSplitContainer/MarginContainer/Gunpowder");
		rabitsfootSprite = GetNode<Sprite2D>("MarginContainer/HSplitContainer/MarginContainer/Rabitsfoot");
		rocketfuelSprite = GetNode<Sprite2D>("MarginContainer/HSplitContainer/MarginContainer/Rocketfuel");
		gasolineSprite = GetNode<Sprite2D>("MarginContainer/HSplitContainer/MarginContainer/Gasoline");

		gunpowderSprite.Visible = false;
		rabitsfootSprite.Visible = false;
		rocketfuelSprite.Visible = false;
		gasolineSprite.Visible = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
	public void _on_item_list_item_selected(int itemID)
	{
		switch (itemID)
		{
			case 0:
				gunpowderSprite.Visible = true;
				rabitsfootSprite.Visible = false;
				rocketfuelSprite.Visible = false;
				gasolineSprite.Visible = false;
				break;
			case 1:
				gunpowderSprite.Visible = false;
				rabitsfootSprite.Visible = true;
				rocketfuelSprite.Visible = false;
				gasolineSprite.Visible = false;			
				break;
			case 2:
				gunpowderSprite.Visible = false;
				rabitsfootSprite.Visible = false;
				rocketfuelSprite.Visible = false;
				gasolineSprite.Visible = true;
				break;
			case 3:
				gunpowderSprite.Visible = false;
				rabitsfootSprite.Visible = false;
				rocketfuelSprite.Visible = true;
				gasolineSprite.Visible = false;
				break;
			
		}
	}
	public void _on_item_list_item_activated(int itemID)
	{
		switch (itemID)
		{
			case 0:

				break;
			case 1:
	
				break;
			case 2:

				break;
			case 3:

				break;
		}
	}
}
