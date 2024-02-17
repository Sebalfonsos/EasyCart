using System.Collections.ObjectModel;
using System.ComponentModel;


namespace EasyCart
{
    public partial class MainPage : ContentPage
    {
        ObservableCollection<TaskItem> tasks;


        public MainPage()
        {
            InitializeComponent();
            tasks = new ObservableCollection<TaskItem>();
            tasksList.ItemsSource = tasks;
        }

        async void OnAddTaskClicked(object sender, EventArgs e)
        {
            string taskName = await DisplayPromptAsync("Nueva Compra", "Ingrese el nombre del producto", "Guardar", "Cancelar", "Nombre del producto", 250);
            if (!string.IsNullOrWhiteSpace(taskName))
            {
                tasks.Add(new TaskItem { TaskName = taskName, IsCompleted = false });
            }
        }


        async void OnTaskSelected(object sender, SelectionChangedEventArgs e)

        {

            if(e.CurrentSelection != null)
        {
                // Get the selected item
                var selectedItem = (TaskItem)e.CurrentSelection[0];
                string action = await DisplayActionSheet("Acciones", "Cancelar", null, "Editar", "Eliminar");
                if (action == "Editar")
                {
                    await EditTask(selectedItem);
                }
                else if (action == "Eliminar")
                {
                    await DeleteTask(selectedItem);
                }
                // Do something with the selected item
                Console.WriteLine($"Selected item: {selectedItem.TaskName}");
            }
        }

        async Task EditTask(TaskItem SelectedTask)
        {
            if (SelectedTask != null)
            {
                string newTaskName = await DisplayPromptAsync("Editar Tarea", "Ingrese el nuevo nombre de la tarea", "Guardar", "Cancelar", "Nombre de la tarea", 250, null, SelectedTask.TaskName);
                if (!string.IsNullOrWhiteSpace(newTaskName))
                {
                    SelectedTask.TaskName = newTaskName;
                    OnPropertyChanged(nameof(SelectedTask.TaskName));
                }
            }
        }

        async Task DeleteTask(TaskItem SelectedTask)
        {
            if (SelectedTask != null)
            {
                tasks.Remove(SelectedTask);
            }
        }




       

        public class TaskItem : INotifyPropertyChanged
        {
            public bool IsCompleted { get; set; }
            private string _taskName;
            public string TaskName
            {
                get { return _taskName; }
                set
                {
                    if (_taskName != value)
                    {
                        _taskName = value;
                        OnPropertyChanged(nameof(TaskName));
                    }
                }
            }


            public event PropertyChangedEventHandler PropertyChanged;
            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }




    }

}
