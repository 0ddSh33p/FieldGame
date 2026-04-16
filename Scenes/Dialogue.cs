using Godot;
using System;
using System.ComponentModel;

public partial class Dialogue : Node
{
	
	[Export] private Control uiPanel;
	[Export] private TextEdit t1, t2, word;
	
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
			int wordS = diaText[cur].Find("$");
			int wordE = diaText[cur].Find("%");

			t1.Text = diaText[cur].Substr(0,wordS);
			word.Text = diaText[cur].Substr(wordS+1,wordE-wordS+1);
			t2.Text = diaText[cur].Substr(wordE+1,diaText[cur].Length - wordE + 1);

			if (Input.IsActionJustPressed("talk"))
			{
				if(cur <= end) cur += 1;
			}
		}
	}

}
