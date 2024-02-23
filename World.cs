using Godot;
using System;
using System.Collections.Generic;

public partial class World : Node2D
{
	
	public PackedScene pelletScene = GD.Load<PackedScene>("res://pellet.tscn");
	public PackedScene playerScene = GD.Load<PackedScene>("res://player.tscn");
	public PackedScene playerBody = GD.Load<PackedScene>("res://player_body.tscn");
	
	public Vector2 ScreenSize;
	
	private List<Area2D> PlayerBody = new List<Area2D>();
	
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
		
		//Check Game Over condition(s)
		var player = GetNode<Player>("Player");
		if (player.Position.X < 0 || player.Position.X >= GetViewportRect().Size.X ||
			player.Position.Y < 0 || player.Position.Y >= GetViewportRect().Size.Y)
		{
			// Game over, handle accordingly
			GD.Print("Game Over!");
			GetTree().Quit();
		}
		
		// Check for body overlap
		/*var Area2DList = player.GetOverlappingAreas();
		foreach (Node2D overlappingArea in Area2DList)
		{
			GD.Print(overlappingArea);
		}*/
		
		// Reset pellet if eaten
		var pellet = GetNode<Pellet>("Pellet");
		if(pellet.GetAlive() == 0)
		{
			RemoveChild(pellet);
			
			// Grow player
			Area2D body = playerBody.Instantiate<Area2D>();
			PlayerBody.Add(body);
			AddChild(body);
			
			var random = new RandomNumberGenerator();
			random.Randomize();
			
			var newX = random.RandiRange(0, (int)ScreenSize.X / 4);
			var newY = random.RandiRange(0, (int)ScreenSize.Y / 4);
			
			pellet.Position = new Vector2(newX, newY);
			pellet.SetAlive(1);
			
			AddChild(pellet);
		}
		
		// Move player tail
		Vector2 temp = player.Position;
		foreach(Area2D bodyPart in PlayerBody)
		{
			RemoveChild(bodyPart);
			Vector2 temp2 = bodyPart.Position;
			bodyPart.Position = new Vector2(temp.X, temp.Y);
			temp = temp2;
			AddChild(bodyPart);
		}
	}
}
