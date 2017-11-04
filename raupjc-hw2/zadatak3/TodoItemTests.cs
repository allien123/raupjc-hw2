using Microsoft.VisualStudio.TestTools.UnitTesting;
using zadatak2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zadatak2.Tests
{
    [TestClass()]
    public class TodoItemTests
    {
        [TestMethod()]
        public void TodoItemTest()
        {
            string itemText = "do some stuff";
            TodoItem item = new TodoItem(itemText);
            Assert.AreEqual(itemText,item.Text);
            Assert.AreEqual(false,item.IsCompleted);
        }

        [TestMethod()]
        public void MarkAsCompletedTest()
        {
            TodoItem item = new TodoItem("do some stuff");
            Assert.IsFalse(item.IsCompleted);
            Assert.IsTrue(item.MarkAsCompleted());
            Assert.IsTrue(item.IsCompleted);
            Assert.IsFalse(item.MarkAsCompleted());
            Assert.IsTrue(item.IsCompleted);
        }

        [TestMethod()]
        public void EqualsTest()
        {
            string itemText = "do some stuff";
            TodoItem item = new TodoItem(itemText);
            Assert.AreNotEqual(item,null);
            TodoItem item2 = new TodoItem("do some other stuff");
            Assert.AreNotEqual(item,item2);
            TodoItem item3 = new TodoItem(itemText);
            Assert.AreNotEqual(item,item3);
            Assert.AreEqual(item,item);
        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            TodoItem item = new TodoItem("do some stuff");
            Assert.AreEqual(item.GetHashCode(),item.GetHashCode());
        }
    }
}