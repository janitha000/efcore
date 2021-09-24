using EFCore.Application.Interfaces;
using EFCore.Application.Models;
using EFCore.Application.Services;
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
        

        public ToDoServiceTests()
        {
            ToDoService = new ToDoService();
        }


        [Fact]
        public void ToggleDone_ShouldToggleDone()
        {
            var inputItem = new TodoItem() { Title = "Test", Done = false };

            var output = ToDoService.ToggleDone(inputItem);

            Assert.True(output.Done);


        }
    }
}
