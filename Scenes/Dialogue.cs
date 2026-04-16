using Godot;
using System;

public partial class Dialogue : Node
{
	
	private Node2D uiPanel;
	public bool running;
	
	
	public void openDiologue(string group, string[] text)
	{
		updateBalloon(group);
		uiPanel.Show();
		Input.MouseMode = Input.MouseModeEnum.Confined;
		running = true;

		

		running = false;
		Input.MouseMode = Input.MouseModeEnum.Captured;
		uiPanel.Hide();
	}
	public void updateBalloon(string group){

	}
}
