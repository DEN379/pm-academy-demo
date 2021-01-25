using NotesApp.Tools;
using System;
using Xunit;
//using FluentAssertions;
namespace XUnitToolsTest
{
    public class NumberGeneratorTest
    {
        [Fact]
        public void Should_Throw_ArgumentException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(()=> NumberGenerator.GeneratePositiveLong(-2));
        }

        [Fact]
        public void Should_Return_Exactly_Length()
        {
            int length = 2;
            long numb = NumberGenerator.GeneratePositiveLong(length);
            Assert.Equal(length, numb.ToString().Length);
        }
    }
}
