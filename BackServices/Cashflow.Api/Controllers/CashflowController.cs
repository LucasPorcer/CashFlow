using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Cashflow.Application.Entry.Command;
using Cashflow.Application.Entry.Query;
using Cashflow.Domain.Common.ViewModel;
using Cashflow.Domain.ViewModel.Cashflow;

namespace Cashflow.Api.Controllers
{   
    [ApiController]
    [Route("[controller]")]
    public class CashflowController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ILogger<CashflowController> _logger;  

        public CashflowController(ILogger<CashflowController> logger, IMediator mediator, IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
        }
        /// <summary>
        /// M�todo responsavel pela gera��o do balan�o di�rio
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "DailyBalance")]
        [ProducesResponseType(typeof(List<DailyBalanceDto>), 200)]
        [ProducesResponseType(typeof(ErrorDto), 500)]
        public async Task<IActionResult> GetDailyBallance()
        {
            try
            {
                var dailyBalance = await _mediator.Send(new DailyBalanceQuery());

                var result = _mapper.Map<List<DailyBalanceDto>>(dailyBalance);

                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                var errorDto = ErrorDto.New($"Erro encontrado: {ex.Message}");

                _logger.LogDebug("Ocorreu um erro durante a geracao do balan�o diario");

                return StatusCode(500, errorDto);
            }
        }
        /// <summary>
        /// M�todo para lan�amento das entradas de debito e credito
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ValidationErrorDto), 400)]
        [ProducesResponseType(typeof(ErrorDto), 500)]
        public async Task<IActionResult> DoEntryAsync(CreateEntryCommand command)
        {
            try
            { 
                await _mediator.Send(command);
                return StatusCode(200);
            }
            catch (ValidationException exception)
            {
                var validationErrors = exception.Errors.Select(e => e.ErrorMessage).ToList();
                var dto = ValidationErrorDto.New(validationErrors);

                return StatusCode(400, dto);
            }
            catch (Exception ex)
            {
                _logger.LogDebug($"Ocorreu um erro ao realizar uma entrada. Erro: {ex.Message}");

                var dto = ErrorDto.New($"Erro encontrado: {ex.Message}");

                return StatusCode(500, dto);
            }
        }
    }
}