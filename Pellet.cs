using Godot;
using System;

public partial class Pellet : Area2D
{
	private int _isAlive = 1;
	
	public Vector2 ScreenSize;
	public void SetAlive(int indicator)
	{
		_isAlive = indicator;
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var random = new RandomNumberGenerator();
		random.Randomize();
		
		// Check if snake 'eats' pellet
		var Area2DList = GetOverlappingAreas();
		
		// Pellet is eaten
		if(Area2DList.Count > 0)
		{
			GD.Print(Area2DList);
			SetAlive(0);
		}
		// Move pellet and reset
		if(_isAlive == 0)
		{
	 		var rndX = random.RandiRange(0, (int)ScreenSize.X);
	 		var rndY = random.RandiRange(0, (int)ScreenSize.Y);
			
			Position = new Vector2(
				x: Mathf.Clamp(rndX, 0, ScreenSize.X),
				y: Mathf.Clamp(rndY, 0, ScreenSize.Y)
			);
			
			SetAlive(1);
		}
	}
}
