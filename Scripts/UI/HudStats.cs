using Godot;
using System;

public partial class HudStats : Label
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SetProcess(true);
	}


	public override void _Process(double delta)
	{
		int fps = (int)Performance.GetMonitor(Performance.Monitor.TimeFps);
		Text = $"FPS: {fps}";
	}
}
