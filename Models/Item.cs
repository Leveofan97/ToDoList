using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Models
{
    public class Item : INotifyPropertyChanged, IDataErrorInfo
    {
        private int id;
        private string description;
        private bool isDone;

        #region constructors
        public Item() { }
        public Item(int id, string description)
        {
            this.id = id;
            this.description = description;
        }
        #endregion
        /// <summary>
        /// id элемента
        /// </summary>
        public virtual int Id {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// описание элемента
        /// </summary>
        public virtual string Description {
            get { return description; }
            set {
                description = value;
                OnPropertyChanged("Описание");
            }
        }

        /// <summary>
        /// чек выполнения элемента
        /// </summary>
        public virtual bool IsDone
        {
            get { return isDone; }
            set
            {
                isDone = value;
                OnPropertyChanged("Готово");
            }
        }

        #region IDataErrorInfo Members
        public virtual string Error
        {
            get;
            set;
        }
        public virtual string this[string columnName]
        {
            get
            {
                if (columnName == "Description")
                {
                    if (String.IsNullOrWhiteSpace(Description))
                    {
                        Error = "Description cannot be null or empty.";
                    }
                    else
                    {
                        Error = null;
                    }
                }

                return Error;
            }
        }
        #endregion

        #region INotifyPropertyChanged Members
        public virtual event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if(handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }

}
