﻿using System;
using System.Threading;
using System.Threading.Tasks;
using School.Commands;
using School.Persistence;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace School.Tests.Unit.Commands
{
    public class EnrollValidatorTests
    {
        [Fact]
        public async Task ValidateAsync_should_fail_when_Student_does_not_exists()
        {
            var repo = NSubstitute.Substitute.For<IStudentsRepository>();
            var unitOfWork = NSubstitute.Substitute.For<ISchoolUnitOfWork>();
            unitOfWork.StudentsRepository.ReturnsForAnyArgs(repo);
            var sut = new EnrollValidator(unitOfWork);

            var command = new Enroll(Guid.NewGuid(), Guid.NewGuid());
            var result = await sut.ValidateAsync(command, CancellationToken.None);
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Errors.Should().Contain(e =>
                e.Context == nameof(Enroll.StudentId) && e.Message.Contains(command.StudentId.ToString()));
        }

        [Fact]
        public async Task ValidateAsync_should_fail_when_Course_does_not_exists()
        {
            var unitOfWork = NSubstitute.Substitute.For<ISchoolUnitOfWork>();
            var sut = new EnrollValidator(unitOfWork);

            var command = new Enroll(Guid.NewGuid(), Guid.NewGuid());
            var result = await sut.ValidateAsync(command, CancellationToken.None);
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Errors.Should().Contain(e =>
                e.Context == nameof(Enroll.CourseId) && e.Message.Contains(command.CourseId.ToString()));
        }

        [Fact]
        public async Task ValidateAsync_should_succeed_when_command_valid()
        {
            var student = new Student(Guid.NewGuid(), "existing", "Student");

            var studentsRepository = NSubstitute.Substitute.For<IStudentsRepository>();
            studentsRepository.FindByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
                .ReturnsForAnyArgs(student);

            var course = new Course(Guid.NewGuid(), "existing course");

            var coursesRepository = NSubstitute.Substitute.For<ICoursesRepository>();
            coursesRepository.FindByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
                .ReturnsForAnyArgs(course);

            var unitOfWork = NSubstitute.Substitute.For<ISchoolUnitOfWork>();
            unitOfWork.StudentsRepository.ReturnsForAnyArgs(studentsRepository);
            unitOfWork.CoursesRepository.ReturnsForAnyArgs(coursesRepository);
            var sut = new EnrollValidator(unitOfWork);

            var command = new Enroll(course.Id, student.Id);
            var result = await sut.ValidateAsync(command, CancellationToken.None);
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }
    }
}
