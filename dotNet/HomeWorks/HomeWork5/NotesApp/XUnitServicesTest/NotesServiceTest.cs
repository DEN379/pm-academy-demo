using Moq;
using NotesApp.Services.Abstractions;
using NotesApp.Services.Models;
using NotesApp.Services.Services;
using NSubstitute;
using System;
using Xunit;

namespace XUnitServicesTest
{
    public class NotesServiceTest
    {
        [Fact]
        public void Should_Get_ArgumentNullException_If_Argument_Null()
        {
            Note note = null;
            var moqStorage = new Mock<INotesStorage>();
            var moqEvent = new Mock<INoteEvents>();
            moqStorage.Setup(n => n.AddNote(note, 1));
            moqEvent.Setup(n => n.NotifyAdded(note, 1));
            var a = new NotesApp.Services.Services.NotesService(moqStorage.Object, moqEvent.Object);
            Assert.Throws<ArgumentNullException>(() => a.AddNote(note, 1));
        }

        [Fact]
        public void Should_Show_Notify_Added()
        {
            var storage = Substitute.For<INotesStorage>();
            var events = Substitute.For<INoteEvents>();
            var note = Substitute.For<Note>();
            NotesService notes = new NotesService(storage,events);
            notes.AddNote(note, 2);
            events.Received().NotifyAdded(note,2);
        }

        [Fact]
        public void Should_Not_Show_Notify_Added()
        {
            var storage = Substitute.For<INotesStorage>();
            var events = Substitute.For<INoteEvents>();
            Note note = null;
            NotesService notes = new NotesService(storage, events);
            Assert.Throws<ArgumentNullException>(()=> notes.AddNote(note, 2));
            events.DidNotReceive().NotifyAdded(note, 2);
        }

        [Fact]
        public void Should_Show_Notify_Deleted()
        {
            var storage = Substitute.For<INotesStorage>();
            var events = Substitute.For<INoteEvents>();
            Guid guid = Guid.NewGuid();
            NotesService notes = new NotesService(storage, events);
            notes.DeleteNote(guid, 2);
            events.DidNotReceive().NotifyDeleted(guid,2);
        }

        [Fact]
        public void Should_Not_Show_Notify_Deleted()
        {
            var storage = Substitute.For<INotesStorage>();
            var events = Substitute.For<INoteEvents>();
            Guid guid = Guid.NewGuid();
            NotesService notes = new NotesService(storage, events);
            notes.DeleteNote(guid, 2);
            events.DidNotReceive().NotifyDeleted(guid, 2);
        }
    }
}
