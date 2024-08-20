using KDrom.Application.MediatR.Makes.Commands.Create;
using KDrom.Application.MediatR.Makes.Commands.Update;
using KDrom.Application.MediatR.Makes.Dtos;
using KDrom.Application.MediatR.Makes.Queries.GetList;
using KDrom.Domain.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KDrom.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[ProducesResponseType<ErrorDto>(StatusCodes.Status400BadRequest)]
[ProducesResponseType<ErrorDto>(StatusCodes.Status500InternalServerError)]
public class MakeController : ControllerBase
{
    private readonly IMediator _mediator;

    public MakeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType<MakeListDto>(StatusCodes.Status200OK)]
    public async Task<ActionResult<MakeListDto>> GetList()
    {
        var request = new GetMakeListQuery();

        var result = await _mediator.Send(request);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType<string>(StatusCodes.Status200OK)]
    public async Task<ActionResult<string>> Create([FromBody]CreateMakeCommand request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Update([FromBody]UpdateMakeCommand request)
    {
        await _mediator.Send(request);
        return NoContent();
    }
}
