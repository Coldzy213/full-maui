using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WeatherApp.Models
{
    public class TodoItem : INotifyPropertyChanged
    {
        public string Task { get; set; }
        public bool IsCompleted { get; set; }

        private string _priority;
        public string Priority
        {
            get => _priority;
            set
            {
                _priority = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(PriorityColor)); // Notify UI to update color
            }
        }

        public string PriorityColor
        {
            get
            {
                return Priority switch
                {
                    "Low" => "Green",
                    "Medium" => "Orange",
                    "High" => "Red",
                    _ => "Black", // Default color
                };
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
