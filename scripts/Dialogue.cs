using Godot;

public partial class Dialogue : Node
{
	
	[Export] private Control uiPanel;
	[Export] private Label t1, t2, word;
	[Export] private ItemList wordChoices;
	
	public bool running, pause = false, overriden = false;
	private int end, cur;
	private string[] diaText;


    public override void _Ready()
    {
        uiPanel.Hide();
    }

	
	public void openDiologue(string group, string[] text, int st, int en)
	{
		updateBalloon(group);
		cur = st;
		uiPanel.Show();
		Input.MouseMode = Input.MouseModeEnum.Confined;
		overriden = false;
		running = true;

		end = en;
		diaText = text;
	}
	public void updateBalloon(string group){
		//style baloon here
		if(group == "GrassPerson")
		{
			word.Modulate = new Color(.33f,.84f,0f);
		}
		else if (group == "RockPerson")
		{
			word.Modulate = new Color(.26f,.33f,.48f);
		}
		else
		{
			word.Modulate = new Color(.9f,.33f,.48f);
		}
	}


	public override void _Process(double delta)
	{
		if (running)
		{
			int wordS = diaText[cur].Find("$");
			int wordE = diaText[cur].Find("%");

			if(wordS >= 0)
			{
				t1.Text = diaText[cur].Substr(0,wordS);
				if (!overriden)
				{
					word.Text = diaText[cur].Substr(wordS+1,wordE-wordS -1);	
				}
				t2.Text = diaText[cur].Substr(wordE+1,diaText[cur].Length - wordE + 1);
			} else
			{
				t1.Text =  diaText[cur];
				word.Text = "";
				t2.Text = "";
			}



			if (Input.IsActionJustPressed("talk") && !pause)
			{
				if(cur < end - 1)
				{
					cur ++;
				}
				else
				{
					running = false;
					Input.MouseMode = Input.MouseModeEnum.Captured;
					uiPanel.Hide();
				}
			}
		}
	}

	public void MouseOverPause()
	{
		pause = true;
	}
	public void MouseOutUnpause()
	{
		pause = false;
	}

	public void overrideWord(int index, Vector2 atPosition, int mouseButton)
	{
		overriden = true;
		word.Text = wordChoices.GetItemText(index);
	}
}
