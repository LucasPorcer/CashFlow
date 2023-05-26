using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Cashflow.Api.Controllers;
using Cashflow.Domain.Entities.Cashflow;
using Cashflow.Tests.Builders.CashFlow;
using Cashflow.Domain.Interfaces.Repository;
using Cashflow.Application.Entry.Query;
using FluentAssertions;

namespace Cashflow.Tests.Handlers.Entrys{

    [TestFixture]
    public class DailyBalanceQueryHandlerTest
    {     
        private Mock<ILogger<CashflowController>> _loggerMock;
        private DailyBalanceQueryHandler _handler;
        private Mock<ICashflowRepository> _repositoryMock;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<CashflowController>>();
            _repositoryMock = new Mock<ICashflowRepository>();

            _loggerMock.Setup(x => x.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()));
        }

        [Test]
        public void ShouldQueryWithSuccess()
        {
            // Arrange
            _handler = new DailyBalanceQueryHandler(_repositoryMock.Object);

            var model = CashFlowBuilder.GetCashflowModelList();

            _repositoryMock.Setup(x => x.GetConsolidatedBalance()).Returns(Task.FromResult(model));

            // Act
            var result = _handler.Handle(It.IsAny<DailyBalanceQuery>(), new CancellationToken()).GetAwaiter().GetResult();

            // Assert
            _repositoryMock.Verify(x => x.GetConsolidatedBalance(), Times.Once);

            result.Should().NotBeNull();
            result.Count().Should().Be(model.Count);
        }

        [Test]
        public void ShouldQueryWithSuccessButWithoutResults()
        {
            // Arrange
            _handler = new DailyBalanceQueryHandler(_repositoryMock.Object);

            _repositoryMock.Setup(x => x.GetConsolidatedBalance()).Returns(Task.FromResult(new List<CashflowModel>()));

            // Act
            var result = _handler.Handle(It.IsAny<DailyBalanceQuery>(), new CancellationToken()).GetAwaiter().GetResult();

            // Assert
            _repositoryMock.Verify(x => x.GetConsolidatedBalance(), Times.Once);

            result.Should().BeEmpty();
            result.Count().Should().Be(0);
        }
    }
}
