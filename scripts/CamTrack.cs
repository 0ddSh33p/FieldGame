using Godot;
using System;

public partial class CamTrack : Node3D
{
	[Export] Vector3 alignAxis;
	private Node3D player;
	public override void _Ready()
	{
		player = GetTree().GetFirstNodeInGroup("Player") as Node3D;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double _delta)
	{
		if(alignAxis == new Vector3(1f, 0, 0))
		{
			Position = new Vector3(player.GlobalPosition.X, Position.Y, Position.Z);
			
		}
		else
		{
			Position = new Vector3(Position.X, Position.Y, player.GlobalPosition.Z);
			
		}
	}
}
