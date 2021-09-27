using EfCore.UnitTests.TestData;
using EFCore.Application.Interfaces;
using EFCore.Application.Models;
using EFCore.Application.Services;
using EFCore.Configurations;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EfCore.UnitTests
{

    public class ToDoServiceTests
    {
        private ToDoService ToDoService;
        private IOptions<BaseConfiguration> _baseOptions;

        public ToDoServiceTests(IOptions<BaseConfiguration> options)
        {
            ToDoService = new ToDoService();
            _baseOptions = options;
        }


        [Fact]
        public void ToggleDone_ShouldToggleDone()
        {
            var inputItem = new TodoItem() { Title = "Test", Done = false };

            var output = ToDoService.ToggleDone(inputItem);

            var options = _baseOptions.Value;

            Assert.True(output.Done);
        }

        [Theory]
        [ClassData(typeof(ToDoTestData))]
        public void ToggleDone_ShouldToggleDone_Theory(TodoItem inputItem, bool expected)
        {
            var result = ToDoService.ToggleDone(inputItem);
            Assert.Equal(expected, result.Done);

        }

        //[Theory]
        //[MemberData(memberName: nameof(GetTodoItems)]
        //public void ToggleDone_ShouldToggleDone_Theory_memberdata(TodoItem inputItem, bool expected)
        //{
        //    var result = ToDoService.ToggleDone(inputItem);
        //    Assert.Equal(expected, result.Done);

        //}
    }
}
