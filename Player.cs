using Godot;
using System;

public partial class Player : Area2D
{
	
	[Export]
	public int Speed { get; set; } = 100;
	
	[Export]
	public float RotationSpeed { get; set; } = 90 * MathF.PI / 180 ;
	
	public Vector2 ScreenSize;
	public Vector2 Velocity = Vector2.Zero;
	
	private float _rotationDirection;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;	
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
			
		// Get snake direction
		_rotationDirection = Input.GetAxis("turn_left", "turn_right");
		
		Rotation += _rotationDirection * RotationSpeed;
		
		//Apply Snake rotation
		Velocity.X = Speed * MathF.Cos(Rotation);
		Velocity.Y = Speed * MathF.Sin(Rotation);
		
		// Player movement
		Position += Velocity * (float)delta;
		Position = new Vector2(
			x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
			y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y)
		);
	}

	
}
