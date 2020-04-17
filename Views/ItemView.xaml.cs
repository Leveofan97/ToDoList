using System.Windows;
using ToDoList.Models;
using ToDoList.ViewModels;

namespace ToDoList.Views
{
    public partial class ItemView : Window
    {

        public ItemView()
        {
            InitializeComponent();
            DataContext = new ItemViewModel();
        }
    }
}
