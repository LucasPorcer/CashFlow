using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Cashflow.Api.Controllers;
using Cashflow.Application.Entry.Command;
using Cashflow.Domain.Entities.Cashflow;
using Cashflow.Tests.Builders.CashFlow;
using Cashflow.Domain.Interfaces.Repository;

namespace Cashflow.Tests.Handlers.Entrys
{
    [TestFixture]
    public class CreateEntryHandlerTest
    {
        private Mock<ILogger<CashflowController>> _loggerMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IValidator<CreateEntryCommand>> _validatorMock;
        private CreateEntryCommandHandler _handler;
        private Mock<ICashflowRepository> _repositoryMock;

        [SetUp]
        public void Setup()
        {
            _validatorMock = new Mock<IValidator<CreateEntryCommand>>();
            _loggerMock = new Mock<ILogger<CashflowController>>();
            _mapperMock = new Mock<IMapper>();
            _repositoryMock = new Mock<ICashflowRepository>();

            _loggerMock.Setup(x => x.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()));
        }

        [Test]
        public void ShouldCreateEntringWithSuccess()
        {
            // Arrange
            _handler = new CreateEntryCommandHandler(_validatorMock.Object, _repositoryMock.Object, _mapperMock.Object);
            _validatorMock.Setup(x => x.ValidateAsync(It.IsAny<CreateEntryCommand>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(new ValidationResult()));


            var model = CashFlowBuilder.CreateGenerticCashFlowModelWithValue();

            _repositoryMock.Setup(x => x.AddAsync(model)).Returns(Task.FromResult(new CashflowModel()));

            var command = new CreateEntryCommand() { Value = 0 };

            // Act
            _handler.Handle(command, new CancellationToken()).GetAwaiter().GetResult();

            // Assert
            _repositoryMock.Verify(x => x.AddAsync(It.IsAny<CashflowModel>()), Times.Once);
        }

        [Test]
        public void ShouldNotCreateEntringWhenHasValidationErrors()
        {
            // Arrange
            var exception = new ValidationException("ErrorMessage", new List<ValidationFailure> {new ValidationFailure(){ ErrorMessage = "ErrorMessage" } });

            _handler = new CreateEntryCommandHandler(_validatorMock.Object, _repositoryMock.Object, _mapperMock.Object);
            _validatorMock.Setup(x => x.ValidateAsync(It.IsAny<CreateEntryCommand>(), It.IsAny<CancellationToken>())).Throws(() => exception);


            var model = CashFlowBuilder.CreateGenerticCashFlowModelWithValue();

            _repositoryMock.Setup(x => x.AddAsync(model)).Returns(Task.FromResult(new CashflowModel()));

            var command = new CreateEntryCommand() { Value = 0 };

            // Act

            ValidationException ex = Assert.Throws<ValidationException>(() => _handler.Handle(command, new CancellationToken()).GetAwaiter().GetResult());

            // Assert
            _repositoryMock.Verify(x => x.AddAsync(It.IsAny<CashflowModel>()), Times.Never);
        }
    }
}
