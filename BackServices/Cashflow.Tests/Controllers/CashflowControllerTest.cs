using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Cashflow.Api.Controllers;
using Cashflow.Application.Entry.Command;
using Cashflow.Application.Entry.Query;
using Cashflow.Domain.ViewModel.Cashflow;
using Cashflow.Domain.Entities.Cashflow;
using Cashflow.Tests.Builders.CashFlow;
using FluentAssertions;

namespace Cashflow.Tests.Controllers
{
    [TestFixture]
    public class CashflowControllerTest
    {
        private Mock<IMediator> _mediatorMock;
        private Mock<IValidator<CreateEntryCommand>> _validatorMock;
        private Mock<ILogger<CashflowController>> _loggerMock;
        private Mock<IMapper> _mapperMock;
        private CashflowController _controller;

        [SetUp]
        public void Setup()
        {
            _mediatorMock = new Mock<IMediator>();
            _validatorMock = new Mock<IValidator<CreateEntryCommand>>();
            _loggerMock = new Mock<ILogger<CashflowController>>();
            _mapperMock = new Mock<IMapper>();
            _controller = new CashflowController(_loggerMock.Object, _mediatorMock.Object, _mapperMock.Object);

            _loggerMock.Setup(x => x.Log(It.IsAny<LogLevel>(),It.IsAny<EventId>(),It.IsAny<It.IsAnyType>(),It.IsAny<Exception>(),(Func<It.IsAnyType, Exception, string>)It.IsAny<object>()));
        }

        [Test]
        public void ShouldDoEntringWithSuccess()
        {
            // Arrange
            _validatorMock.Setup(x => x.ValidateAsync(It.IsAny<CreateEntryCommand>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(new ValidationResult()));
            _mediatorMock.Setup(x => x.Send(It.IsAny<CreateEntryCommand>(), It.IsAny<CancellationToken>()));

            // Act
            var result = _controller.DoEntryAsync(It.IsAny<CreateEntryCommand>()).GetAwaiter().GetResult();
            var controllerResult = (StatusCodeResult)result;

            // Assert
            _mediatorMock.Verify(x => x.Send(It.IsAny<CreateEntryCommand>(), It.IsAny<CancellationToken>()), Times.Once);

            controllerResult.StatusCode.Should().Be(200);
        }

        [Test]
        public void ShouldNotDoEntringWithValidationErrorsWhenValueIsZero()
        {
            var validationException = new ValidationException(String.Empty, new List<ValidationFailure> { new ValidationFailure(){ ErrorMessage = "Campo Valor deve ser diferente de 0." },});

            var command = new CreateEntryCommand() { Value = 0};

            // arrange
            _validatorMock.Setup(x => x.ValidateAsync(command, It.IsAny<CancellationToken>()))
                .Throws(() => validationException);

            _mediatorMock.Setup(x => x.Send(It.IsAny<CreateEntryCommand>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(new ValidationException("hey")));

            // act
            var result = _controller.DoEntryAsync(command).GetAwaiter().GetResult();

            var controllerResult = (StatusCodeResult)result;

            // assert
            controllerResult.StatusCode.Should().Be(200);
        }

        [Test]
        public void ShouldReturnConsolidatedBalanceWithSuccess()
        {
            // Arrange

            var balances = CashFlowBuilder.CreateDailyBalances(3);

            _mediatorMock.Setup(x => x.Send(It.IsAny<DailyBalanceQuery>(), It.IsAny<CancellationToken>()));

            _mapperMock.Setup(x => x.Map<IList<DailyBalanceDto>>(It.IsAny<IList<CashflowModel>>()))
                .Returns(() => balances);

            // act
            var result = _controller.GetDailyBallance().GetAwaiter().GetResult();

            var controllerResult = (ObjectResult)result;

            var viewModelSuccess = controllerResult.Value as IList<DailyBalanceDto>;

            // assert
            controllerResult.StatusCode.Should().Be(200);

            viewModelSuccess[0].Balance.Should().Be(balances[0].Balance);
            viewModelSuccess[0].Date.Date.Should().Be(balances[0].Date.Date);
           
            viewModelSuccess[1].Balance.Should().Be(balances[1].Balance);
            viewModelSuccess[1].Date.Date.Should().Be(balances[1].Date.Date);

            viewModelSuccess[2].Balance.Should().Be(balances[2].Balance);
            viewModelSuccess[2].Date.Date.Should().Be(balances[2].Date.Date);
        }

        [Test]
        public void ShouldReturnConsolidatedBalanceWithError()
        {
            // arrange
            _mediatorMock.Setup(x => x.Send(It.IsAny<DailyBalanceQuery>(), It.IsAny<CancellationToken>()));
            _mapperMock.Setup(x => x.Map<IList<DailyBalanceDto>>(It.IsAny<IList<CashflowModel>>())).Throws<Exception>();

            // act
            var result = _controller.GetDailyBallance().GetAwaiter().GetResult();

            var controllerResult = (ObjectResult)result;

            var viewModelSuccess = controllerResult.Value as IList<DailyBalanceDto>;

            // assert
            controllerResult.StatusCode.Should().Be(500);
        }
    }
}