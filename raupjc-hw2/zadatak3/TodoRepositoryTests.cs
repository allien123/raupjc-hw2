using Microsoft.VisualStudio.TestTools.UnitTesting;
using zadatak2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using zadatak2.collections;

namespace zadatak2.Tests
{
    [TestClass()]
    public class TodoRepositoryTests
    {
        [TestMethod()]
        public void TodoRepositoryTest()
        {
            TodoRepository repo = new TodoRepository();
            TodoRepository repo2 = new TodoRepository(new GenericList<TodoItem>());

            //just checking if the repos have been correctly initialized - if they haven't, this method calls should throw a null pointer exception
            repo.GetAll();
            repo2.GetAll();
        }

        [TestMethod()]
        public void GetTest()
        {
            TodoItem item1 = new TodoItem("do some stuff");
            TodoItem item2 = new TodoItem("do some other stuff");
            TodoItem item3 = new TodoItem("do your C# homework");
            IGenericList<TodoItem> items = new GenericList<TodoItem>();
            items.Add(item1);
            items.Add(item2);

            TodoRepository repo = new TodoRepository(items);
            Assert.AreEqual(item1,repo.Get(item1.Id));
            Assert.AreEqual(item2, repo.Get(item2.Id));
            Assert.IsNull(repo.Get(item3.Id));
        }

        [TestMethod()]
        public void AddTest()
        {
            TodoRepository repo = new TodoRepository();
            TodoItem item = new TodoItem("do some stuff");
            repo.Add(item);
            Assert.AreEqual(item,repo.Get(item.Id));
        }

        [TestMethod()]
        [ExpectedException(typeof(DuplicateTodoItemException))]
        public void AddExistingItemTest()
        {
            TodoRepository repo = new TodoRepository();
            TodoItem item = new TodoItem("do some stuff");
            repo.Add(item);
            repo.Add(item);
        }

        [TestMethod()]
        public void RemoveTest()
        {
            TodoRepository repo = new TodoRepository();

            TodoItem item = new TodoItem("do some stuff");
            repo.Add(item);
            Assert.IsTrue(repo.Remove(item.Id));
            Assert.IsNull(repo.Get(item.Id));
            Assert.IsFalse(repo.Remove(item.Id));
        }

        [TestMethod()]
        public void UpdateTest()
        {
            TodoRepository repo = new TodoRepository();

            TodoItem item = new TodoItem("do some stuff");
            repo.Add(item);

            string itemText = "do some other stuff";
            TodoItem item2 = new TodoItem(itemText);
            item2.Id = item.Id;
            repo.Update(item2);

            TodoItem itemFromRepo = repo.Get(item.Id);
            Assert.AreEqual(itemText,itemFromRepo.Text);
        }

        [TestMethod()]
        public void UpdateUnexistingItemTest()
        {
            TodoRepository repo = new TodoRepository();

            TodoItem item = new TodoItem("do some stuff");

            string itemText = "do some other stuff";
            TodoItem item2 = new TodoItem(itemText);
            item2.Id = item.Id;
            repo.Update(item2);

            TodoItem itemFromRepo = repo.Get(item.Id);
            Assert.AreEqual(itemText, itemFromRepo.Text);
        }

        [TestMethod()]
        public void MarkAsCompletedTest()
        {
            TodoRepository repo = new TodoRepository();

            TodoItem item = new TodoItem("do some stuff");
            repo.Add(item);

            Assert.IsTrue(repo.MarkAsCompleted(item.Id));
            Assert.IsTrue(item.IsCompleted);
            Assert.IsFalse(repo.MarkAsCompleted(item.Id));

            TodoItem item2 = new TodoItem("do some other stuff");
            Assert.IsFalse(repo.MarkAsCompleted(item2.Id));
        }

        [TestMethod()]
        public void GetAllTest()
        {
            TodoItem item1 = new TodoItem("do some stuff");
            TodoItem item2 = new TodoItem("do some other stuff");
            TodoItem item3 = new TodoItem("do your C# homework");
            TodoRepository repo = new TodoRepository();
            repo.Add(item1);
            repo.Add(item2);
            repo.Add(item3);

            item1.DateCreated = new DateTime(2005,12,25);
            item2.DateCreated = new DateTime(2012,5,28);
            item3.DateCreated = new DateTime(2015,7,21);

            List<TodoItem> items = repo.GetAll();
            Assert.AreEqual(item3,items.ElementAt(0));
            Assert.AreEqual(item2, items.ElementAt(1));
            Assert.AreEqual(item1, items.ElementAt(2));
        }

        [TestMethod()]
        public void GetActiveTest()
        {
            TodoItem item1 = new TodoItem("do some stuff");
            TodoItem item2 = new TodoItem("do some other stuff");
            TodoItem item3 = new TodoItem("do your C# homework");
            TodoItem item4 = new TodoItem("study some subject");
            TodoRepository repo = new TodoRepository();
            repo.Add(item1);
            repo.Add(item2);
            repo.Add(item3);
            repo.Add(item4);

            Assert.AreEqual(4,repo.GetActive().Count);
            item1.MarkAsCompleted();
            Assert.AreEqual(3, repo.GetActive().Count);
            item2.MarkAsCompleted();
            Assert.AreEqual(2, repo.GetActive().Count);

            Assert.IsTrue(repo.GetActive().Contains(item3));
            Assert.IsTrue(repo.GetActive().Contains(item4));
            Assert.IsFalse(repo.GetActive().Contains(item1));
            Assert.IsFalse(repo.GetActive().Contains(item2));
        }

        [TestMethod()]
        public void GetCompletedTest()
        {
            TodoItem item1 = new TodoItem("do some stuff");
            TodoItem item2 = new TodoItem("do some other stuff");
            TodoItem item3 = new TodoItem("do your C# homework");
            TodoItem item4 = new TodoItem("study some subject");
            TodoRepository repo = new TodoRepository();
            repo.Add(item1);
            repo.Add(item2);
            repo.Add(item3);
            repo.Add(item4);

            Assert.AreEqual(0, repo.GetCompleted().Count);
            item1.MarkAsCompleted();
            Assert.AreEqual(1, repo.GetCompleted().Count);
            item2.MarkAsCompleted();
            Assert.AreEqual(2, repo.GetCompleted().Count);

            Assert.IsTrue(repo.GetCompleted().Contains(item1));
            Assert.IsTrue(repo.GetCompleted().Contains(item2));
            Assert.IsFalse(repo.GetCompleted().Contains(item3));
            Assert.IsFalse(repo.GetCompleted().Contains(item4));
        }

        [TestMethod()]
        public void GetFilteredTest()
        {
            TodoItem item1 = new TodoItem("do some stuff");
            TodoItem item2 = new TodoItem("do some other stuff");
            TodoItem item3 = new TodoItem("do your C# homework");
            TodoItem item4 = new TodoItem("study some subject");
            TodoRepository repo = new TodoRepository();
            repo.Add(item1);
            repo.Add(item2);
            repo.Add(item3);
            repo.Add(item4);

            item1.DateCreated = new DateTime(2005, 12, 25);
            item2.DateCreated = new DateTime(2012, 5, 28);
            item3.DateCreated = new DateTime(2015, 7, 21);
            item4.DateCreated = new DateTime(2016, 8, 15);

            List<TodoItem> items = repo.GetFiltered(item => item.DateCreated.Month > 6);
            Assert.AreEqual(3,items.Count);
            Assert.IsTrue(items.Contains(item1));
            Assert.IsTrue(items.Contains(item3));
            Assert.IsTrue(items.Contains(item4));

            items = repo.GetFiltered(item => item.Text.StartsWith("do"));
            Assert.AreEqual(3, items.Count);
            Assert.IsTrue(items.Contains(item1));
            Assert.IsTrue(items.Contains(item2));
            Assert.IsTrue(items.Contains(item3));
        }
    }
}