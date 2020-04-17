using System.Collections.ObjectModel;
using System.Windows.Input;
using ToDoList.Commands;
using ToDoList.Models;
using ToDoList.DB;

namespace ToDoList.ViewModels
{
    class ItemViewModel
    {
        private Item item;
        private ObservableCollection<Item> itemsList;
        private ObservableCollection<Item> itemsHistoryList;
        private readonly DBContext database;

        public ItemViewModel()
        {

            database = new DBContext();

            item = new Item() { Description = "" };

            itemsList = new ObservableCollection<Item>();
            itemsHistoryList = new ObservableCollection<Item>();

            initLists();

            AddCommand = new ItemCommand(InsertItem);
            DelCommand = new ItemCommand(RemoveItem);
            DoneCommand = new ItemCommand(SetDoneItem);

        }
        #region properties
        public Item Item
        {
            get { return item; }
        }

        public ObservableCollection<Item> ItemsHistoryList
        {
            get { return itemsHistoryList; }
        }

        public ObservableCollection<Item> ItemsList
        {
            get { return itemsList; }
        }
        #endregion

        
        private void initLists()
        {
            RefreshData();
        }

        
        public void RefreshData()
        {
            itemsList.Clear();
            foreach (Item i in database.GetItems(false))
                itemsList.Add(i);

            itemsHistoryList.Clear();
            foreach (Item i in database.GetItems(true))
                itemsHistoryList.Add(i);
        }

        #region CRUD 
        /// <summary>
        /// вставка в бд
        /// </summary>
        /// <param name="item">item to insert</param>
        public void InsertItem(Item item)
        {
            database.InsertItem(item);
            item.Description = "";
            RefreshData();
        }

        /// <summary>
        /// удаление из бд
        /// </summary>
        /// <param name="item">Item to remove</param>
        public void RemoveItem(Item item)
        {
            database.RemoveItem(item);
            RefreshData();
        }

        /// <summary>
        ///  метка готовности
        /// </summary>
        /// <param name="item">item to update</param>
        private void SetDoneItem(Item item)
        {
            item.IsDone = !item.IsDone;
            database.UpdateItem(item);
            RefreshData();
        }
        #endregion

        #region commands
        public ICommand AddCommand
        {
            get;
            private set;
        }
        public ICommand DelCommand
        {
            get;
            private set;
        }
        public ICommand DoneCommand
        {
            get;
            private set;
        }
        #endregion

        /// <summary>
        /// сохранение по нажатию "Добавить"
        /// </summary>
        public void SaveChanges()
        {
            InsertItem(item);
            RefreshData();
        }

        /// <summary>
        /// удаление по нажатию "Удалить"
        /// </summary>
        /// <param name="id">id of selected row</param>
        public void DelItem(int id)
        {
            for (int i = 0; i < itemsList.Count; i++)
                if (itemsList[i].Id == id) RemoveItem(itemsList[i]);
        }

        /// <summary>
        /// выполнение по нажатию "Выполненно"
        /// </summary>
        /// <param name="id">id of selected row</param>
        public void SetDoneItem(int id)
        {
            SetDoneItem(database.GetItem(id));
        }

    }
}