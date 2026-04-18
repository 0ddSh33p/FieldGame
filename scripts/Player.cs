using Godot;
using System;

public partial class Player : CharacterBody3D
{
	[Export] private float speed = 5.0f;
	[Export] public float mSensitivity = 10.0f;
	private Dialogue dio;

    public override void _Ready()
    {
        Input.MouseMode = Input.MouseModeEnum.Captured;
		dio = GetTree().GetFirstNodeInGroup("Dialogue") as Dialogue;
    }

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseMotion motion && !dio.running)
		{
			Vector2 mouseDelta = motion.Relative;
			RotateY(mouseDelta.X * -0.001f * mSensitivity);
		}
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("esc"))
		{
			Input.MouseMode = Input.MouseModeEnum.Visible;
		}
	}


	public override void _PhysicsProcess(double delta)
	{
		if (!dio.running)
		{
			Vector3 velocity = Velocity;

			// Add the gravity.
			if (!IsOnFloor())
			{
				velocity += GetGravity() * (float)delta;
			}

			Vector2 inputDir = Input.GetVector("left", "right", "forward", "back");
			Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
			if (direction != Vector3.Zero)
			{
				velocity.X = direction.X * speed;
				velocity.Z = direction.Z * speed;
			}
			else
			{
				velocity.X = Mathf.MoveToward(Velocity.X, 0, speed);
				velocity.Z = Mathf.MoveToward(Velocity.Z, 0, speed);
			}

			Velocity = velocity;
			MoveAndSlide();
		}
		
	}
}
