using Godot;
using System.Collections.Generic;

public partial class WordController : Control
{
	private List<string> savedWords;
	[Export] private Label word;
	[Export] private ItemList selectables;
	
	public override void _Ready()
	{
		selectables.Hide();
		savedWords = new List<string>();
	}

	public override void _Process(double delta)
	{
		if (word.Text != "" && !savedWords.Contains(word.Text))
		{
			savedWords.Add(word.Text);
			selectables.AddItem(word.Text);
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
