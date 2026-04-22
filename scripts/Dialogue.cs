using System.Collections.Generic;
using Godot;

public partial class Dialogue : Node
{
	
	[Export] private Control uiPanel;
	[Export] private Label t1, t2, word;
	[Export] private ItemList wordChoices;
	[Export] private AnimationPlayer anims;
	
	public bool running, pause = false, overriden = false;
	private int end;
	public int cur, people;
	private string[] diaText;
	public int[] biasLevel, curID;

	public List<int> unlockedIDs;


	public override void _Ready()
	{
		uiPanel.Hide();
		unlockedIDs = new List<int>();
	}

	
	public void openDiologue(string group, string[] text, int st, int en,int[] bias, int[] id)
	{
		updateBalloon(group);
		cur = st;
		curID = id;
		end = en;
		diaText = text;
		biasLevel = bias;

		uiPanel.Show();
		anims.Play("show");
		
		Input.MouseMode = Input.MouseModeEnum.Confined;
		overriden = false;
		running = true;
	}
	
	// calls close anim
	public void closeDialogue()
	{
		anims.Play("close");
	}
	
	// called at the end of close anim just in case
	public void fullyCloseDialogue()
	{
		uiPanel.Hide();
	}

	public void updateBalloon(string group){
		//style baloon here
		if(group == "GrassPerson")
		{
			people = 0;
			word.Modulate = new Color(.33f,.84f,0f);
		}
		else if (group == "RockPerson")
		{
			people = 1;
			word.Modulate = new Color(.46f,.53f,.98f);
		}
		else
		{
			people= -1;
			word.Modulate = new Color(.9f,.33f,.48f);
		}
	}


	public override void _Process(double delta)
	{
		if (running)
		{
			if (! unlockedIDs.Contains(curID[cur]))
			{
				unlockedIDs.Add(curID[cur]);
			}
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
					overriden = false;
					cur ++;
				}
				else
				{
					running = false;
					t1.Text = "";
					word.Text = "";
					t2.Text = "";
					Input.MouseMode = Input.MouseModeEnum.Captured;
					closeDialogue();
				}
			}
		} else
		{
			biasLevel = null;
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


	public int[] unlockedIdFetch()
	{
		int[] retArray = new int[unlockedIDs.Count];
		unlockedIDs.CopyTo(retArray);

		return retArray;
	}
}
