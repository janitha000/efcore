using EFCore.Application.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCore.UnitTests.TestData
{
    public class TestDataGenrator : IEnumerable<object[]>
    {
        public static IEnumerator<object[]> GetTodoItems()
        {
            yield return new object[] { new TodoItem() { Title = "Test1", Done = true }, false };
            yield return new object[] { new TodoItem() { Title = "Test2", Done = true }, false };
            yield return new object[] { new TodoItem() { Title = "Test3", Done = false }, true };
        }

        public IEnumerator<object[]> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
