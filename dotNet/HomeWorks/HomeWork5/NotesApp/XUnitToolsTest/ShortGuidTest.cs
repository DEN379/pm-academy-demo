using NotesApp.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitToolsTest
{
    public class ShortGuidTest
    {
        [Fact]
        public void Shoud_Converting_Work_Propertly()
        {
            string guid = "Ih7B70ZU4kuYwW64XTN1gg";
            var g = guid.FromShortId();
            Assert.NotNull(g);

            var shortGuid = g.Value.ToShortId();
            Assert.NotNull(shortGuid);
        }

        [Fact]
        public void Shoud_Work_If_Add_Equals_Sign_To_Result_Short_Id()
        {
            var guid = Guid.NewGuid().ToShortId() + "==";
            Assert.NotNull(guid.FromShortId());
        }

        [Fact]
        public void Shoud_Convert_String_To_Guid()
        {
            Guid guid = Guid.NewGuid();

            Assert.Equal(guid, guid.ToString().FromShortId());
        }

        [Fact]
        public void Should_Throw_FormatException()
        {
            string guid = "sadf";
            Assert.Throws<FormatException>(() => guid.FromShortId());
        }

        [Fact]
        public void Shoud_Return_Null_If_Argument_Null()
        {
            String guid = null;

            Assert.Null(guid.FromShortId());
        }


    }
}
