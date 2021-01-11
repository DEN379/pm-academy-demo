﻿namespace School.Persistence
{
    public interface ISchoolUnitOfWork : IUnitOfWork
    {
        ICoursesRepository CoursesRepository { get; }
        IStudentsRepository StudentsRepository { get; }
        IMessagesRepository MessagesRepository { get; }
    }
}