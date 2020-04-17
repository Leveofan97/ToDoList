using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using ToDoList.Models;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ToDoList.DB
{
    public class DBContext
    {
        private Configuration HBMConfig;

        public DBContext()
        {
            CreateConnection();

        }
        public void CreateConnection()
        {
            HBMConfig = new Configuration();

            HBMConfig.DataBaseIntegration(x =>
            {
                x.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\SQLDatabase\\Database.mdf;Integrated Security=True";
                x.Driver<SqlClientDriver>();
                x.Dialect<MsSql2012Dialect>();
            });

            HBMConfig.AddAssembly(Assembly.GetExecutingAssembly());


        }

        #region CRUD operations
        /// <summary>
        /// insert item to database
        /// </summary>
        /// <param name="item">item to insert</param>
        internal void InsertItem(Item item)
        {
            var sessionFactory = HBMConfig.BuildSessionFactory();
            using (var session = sessionFactory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    var list = session.Save(item);
                    tx.Commit();
                }
            }
        }

        /// <summary>
        /// get item by id
        /// </summary>
        /// <param name="id">item id</param>
        /// <returns>one item found by id</returns>
        internal Item GetItem(int id)
        {
            var sessionFactory = HBMConfig.BuildSessionFactory();
            using (var session = sessionFactory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    Item item = session.Query<Item>()
                         .Where(x => x.Id == id)
                        .FirstOrDefault();
                    tx.Commit();
                    return item;
                }
            }
        }

        /// <summary>
        /// get list of item done or undone 
        /// </summary>
        /// <param name="isDone">determines which records are needed</param>
        /// <returns></returns>
        public IEnumerable<Item> GetItems(bool isDone)
        {
            var sessionFactory = HBMConfig.BuildSessionFactory();
            using (var session = sessionFactory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    var list = session.Query<Item>()
                         .Where(x => x.IsDone == isDone)
                        .ToList<Item>();
                    tx.Commit();
                    return (List<Item>)list;
                }
            }
        }

        /// <summary>
        /// update item in database
        /// </summary>
        /// <param name="item">item to update</param>
        internal void UpdateItem(Item item)
        {
            var sessionFactory = HBMConfig.BuildSessionFactory();
            using (var session = sessionFactory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    session.Update(item);
                    tx.Commit();
                }
            }
        }

        /// <summary>
        /// remove item form database
        /// </summary>
        /// <param name="item">item to remove</param>
        internal void RemoveItem(Item item)
        {
            var sessionFactory = HBMConfig.BuildSessionFactory();
            using (var session = sessionFactory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    session.Delete(item);
                    tx.Commit();
                }
            }
        }
        #endregion
    }
}
