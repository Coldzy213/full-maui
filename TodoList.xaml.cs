namespace WeatherApp;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using WeatherApp.Models;


public partial class TodoList : ContentPage
{
	public ObservableCollection<TodoItem> Tasks { get; set; } = new();

	public TodoList()
	{
		InitializeComponent();

		BindingContext = this;
	}

	 private void OnAddTaskClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TaskEntry.Text) && PriorityPicker.SelectedItem != null)
            {
                Tasks.Add(new TodoItem
                {
                    Task = TaskEntry.Text,
                    IsCompleted = false,
                    Priority = PriorityPicker.SelectedItem.ToString()
                });

                TaskEntry.Text = string.Empty;
                PriorityPicker.SelectedItem = null;
            }
        }

	private void OnDeleteTaskClicked(object sender, EventArgs e)
	{
		if (sender is Button button && button.CommandParameter is TodoItem task)
		{
			Tasks.Remove(task);
		}
	}
}

