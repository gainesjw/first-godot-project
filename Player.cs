using Godot;
using System;

public partial class Player : Area2D
{
	
	[Export]
	public int Speed { get; set; } = 100;	
	public Vector2 ScreenSize;
	public Vector2 Velocity = Vector2.Zero;
	
	private float _rotationAngle { get; set; } = 90 * MathF.PI / 180 ;
	private float _turnDirection;
	private float _turnQueau;
	
	// Input control
	public override void _Input(InputEvent @event)
	{		
		if(@event.IsPressed())
		{
			_turnQueau += Input.GetAxis("turn_left", "turn_right");
		}
		if(@event.IsReleased())
		{
			_turnDirection = _turnQueau;
		}
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Apply snake direction		
		Rotation = _turnDirection * _rotationAngle;
		Velocity.X = Speed * MathF.Cos(Rotation);
		Velocity.Y = Speed * MathF.Sin(Rotation);
		
		// Snake movement
		Position += Velocity * (float)delta;
		Position = new Vector2(
			x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
			y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y)
		);
		
	}

	
}
