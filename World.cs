using Godot;
using System;

public partial class World : Node2D
{
	
	public PackedScene pelletScene = GD.Load<PackedScene>("res://pellet.tscn");
	public PackedScene playerScene = GD.Load<PackedScene>("res://player.tscn");
	public Vector2 ScreenSize;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;
		AddChild(playerScene.Instantiate());
		AddChild(pelletScene.Instantiate());
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
		var pellet = GetNode<Pellet>("Pellet");
		//GD.Print(pellet.GetAlive());
		
		if(pellet.GetAlive() == 0)
		{
			RemoveChild(pellet);
			
			var random = new RandomNumberGenerator();
			random.Randomize();
			
			var newX = random.RandiRange(0, (int)ScreenSize.X / 2);
			var newY = random.RandiRange(0, (int)ScreenSize.Y / 2);
			
			pellet.Position = new Vector2(newX, newY);
			pellet.SetAlive(1);
			
			AddChild(pellet);
		}
	}
}
