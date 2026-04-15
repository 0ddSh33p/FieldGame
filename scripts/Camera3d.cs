using Godot;
using System;

public partial class Camera3d : Camera3D
{
	[Export] private Player player;
	[Export] private float maxRotationDegs = 75, minRotationDegs = -90;

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseMotion motion)
		{
			Vector2 mouseDelta = motion.Relative;
			RotateX(mouseDelta.Y * -0.001f * player.mSensitivity);
		}
	
		if(RotationDegrees.X > maxRotationDegs)
		{
			RotationDegrees = new Vector3(maxRotationDegs, RotationDegrees.Y, RotationDegrees.Z);
		}
		if(RotationDegrees.X < minRotationDegs)
		{
			RotationDegrees = new Vector3(minRotationDegs, RotationDegrees.Y, RotationDegrees.Z);
		}
	}

}
