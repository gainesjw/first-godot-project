using Godot;
using System;

public partial class Pellet : Area2D
{
	private int _isAlive = 1;
	
	public void SetAlive(int indicator)
	{
		_isAlive = indicator;
	}
	
	public int GetAlive()
	{
		return _isAlive;
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{		
		// Check if snake 'eats' pellet
		var Area2DList = GetOverlappingAreas();
		
		// Pellet is eaten
		if(Area2DList.Count > 0)
		{
			//GD.Print(Area2DList);
			SetAlive(0);
		}
	}
}
