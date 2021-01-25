using NotesApp.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitToolsTest
{
    public class StringGeneratorTest
    {
        [Fact]
        public void Should_Return_Blank_String_If_Length_Is_Zero()
        {
            int length = 0;
            var str = StringGenerator.GenerateNumbersString(length, true);
            Assert.Equal(string.Empty, str);
        }

        [Fact]
        public void Should_Throw_ArgumentException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => StringGenerator.GenerateNumbersString(-2, true));
        }

        [Fact]
        public void Should_Generete_String_Without_Zero()
        {
            bool isZero = false;
            var str = StringGenerator.GenerateNumbersString(4, isZero);
            Assert.NotEqual('0', str[0]);
        }

        [Fact]
        public void Should_Return_Exactly_Length()
        {
            int length = 2;
            var str = StringGenerator.GenerateNumbersString(length, false);
            Assert.Equal(length, str.Length);
        }

        [Fact]
        public void Should_Return_Valid_String_That_May_Conver_To_Number_Type()
        {
            int length = 2;
            var str = StringGenerator.GenerateNumbersString(length, false);
            Type longType = typeof(long);
            Assert.IsAssignableFrom(longType, long.Parse(str));
        }
    }
}
