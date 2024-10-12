using Godot;
using System;

public partial class CameraManager : Node2D
{
	[Export] BirdController bird;
	[Export] Camera2D camera2D;
	public override void _Ready()
	{
		camera2D.Position = new Vector2(camera2D.Position.X, GetViewport().GetVisibleRect().Size.Y / 2);
	}

	public override void _Process(double delta)
	{
		camera2D.Position = new Vector2(bird.Position.X, camera2D.Position.Y);
	}
}
