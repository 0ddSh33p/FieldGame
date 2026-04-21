using Godot;
using System;
using System.Collections.Generic;

public partial class WordController : Control
{
	private List<string> savedWords;
	private List<int> savedBiases;
	[Export] private Label word;
	[Export] private ItemList selectables;
	private Dialogue dia;

	private string lastWord = "";
	public int antiRockBias = 5, antiGrassBias = 5;
	
	public override void _Ready()
	{
		selectables.Hide();
		savedWords = new List<string>();
		savedBiases = new List<int>();
		dia = GetTree().GetFirstNodeInGroup("Dialogue") as Dialogue;
	}

	public override void _Process(double delta)
	{
		if (word.Text != "" && !savedWords.Contains(word.Text))
		{
			savedBiases.Add(dia.biasLevel[dia.cur]);
			savedWords.Add(word.Text);
			selectables.AddItem(word.Text);
		}

		if(lastWord != "" && lastWord != word.Text)
		{
			if(dia.people == 0)
			{
				antiRockBias += savedBiases[savedWords.FindIndex(x => x.Equals(lastWord))];
			}
			if(dia.people == 1)
			{
				antiGrassBias += savedBiases[savedWords.FindIndex(x => x.Equals(lastWord))];
			}
			
		}

		if(word.Text == "")
		{
			selectables.Hide();
		}
		else if(savedWords.Count > 0)
		{
			selectables.Show();
		}
	}
}
