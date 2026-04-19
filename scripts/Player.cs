using Godot;
using System;

public partial class Player : CharacterBody3D
{
	[Export] private float speed = 5.0f, xBound, zBound, xKick, zKick;
	[Export] public float mSensitivity = 10.0f;
	private Dialogue dio;

    public override void _Ready()
    {
		//hide the mouse and ensure it stays on screen, then link to the dialogue manager object
        Input.MouseMode = Input.MouseModeEnum.Captured;
		dio = GetTree().GetFirstNodeInGroup("Dialogue") as Dialogue;
    }

	public override void _Input(InputEvent @event)
	{
		//rotate the player around the vertical axis with the mouse movment
		if (@event is InputEventMouseMotion motion && !dio.running)
		{
			Vector2 mouseDelta = motion.Relative;
			RotateY(mouseDelta.X * -0.001f * mSensitivity);
		}
	}

	public override void _Process(double delta)
	{
		//just for editor use, allows the user to make the mouse visible to quit testing
		if (Input.IsActionJustPressed("esc"))
		{
			Input.MouseMode = Input.MouseModeEnum.Visible;
		}

		//looping player position, this should probably be seperated out onto another object, but I am feeling lazy...
		if(Position.X > xBound)
		{
			Position = new Vector3(Position.X - xKick, Position.Y, Position.Z);
		}
		else if(Position.X < -xBound)
		{
			Position = new Vector3(Position.X + xKick, Position.Y, Position.Z);
		}

		if(Position.Z > zBound)
		{
			Position = new Vector3(Position.X, Position.Y, Position.Z - zKick);
		}
		else if(Position.Z < -zBound)
		{
			Position = new Vector3(Position.X, Position.Y, Position.Z + zKick);
		}
	}


	public override void _PhysicsProcess(double delta)
	{
		// make sure player isn't in dialogue, then ue fairly defaut movment
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
