using Godot;
using System;

public partial class Dialogue : Node
{
	
	private Control uiPanel;
	private TextEdit t1, t2, word;
	
	public bool running;
	private int start, end, cur;
	private string[] diaText;
	
	
	public void openDiologue(string group, string[] text, int st, int en)
	{
		updateBalloon(group);
		cur = start;
		uiPanel.Show();
		Input.MouseMode = Input.MouseModeEnum.Confined;
		running = true;

		end = en;
		diaText = text;

		running = false;
		Input.MouseMode = Input.MouseModeEnum.Captured;
		uiPanel.Hide();
	}
	public void updateBalloon(string group){

	}


    public override void _Process(double delta)
    {
		if (running)
		{
			
			//myText.Text = diaText[cur];
			if (Input.IsActionJustPressed("talk"))
			{
				if(cur <= end) cur += 1;
			}
		}
    }

}
