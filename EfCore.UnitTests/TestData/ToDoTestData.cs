using EFCore.Application.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCore.UnitTests.TestData
{
    public class ToDoTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new TodoItem() { Title = "Test1", Done = true }, false };
            yield return new object[] { new TodoItem() { Title = "Test2", Done = true }, false };
            yield return new object[] { new TodoItem() { Title = "Test3", Done = false }, true };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public TodoItem createNewTodoItem()
        {
            return new TodoItem() { Title = "Test1", Done = false };
        }
    }
}
