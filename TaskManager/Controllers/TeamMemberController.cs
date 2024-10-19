using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Abstractions;
using TaskManager.Contracts.Tasks;
using TaskManager.Contracts.TeamMembers;
using TaskManager.Services;

namespace TaskManager.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TeamMemberController(ITeamMemberService teamMemberService ) : ControllerBase
{
    private readonly ITeamMemberService _teamMemberService = teamMemberService;

    [HttpGet("")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _teamMemberService.GetAllAsync(cancellationToken);
        return Ok(result);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken)
    {
        var result = await _teamMemberService.GetAsync(id, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost("")]
    public async Task<IActionResult> Add([FromBody] TeamMembersRequest request, CancellationToken cancellationToken)
    {
        var result = await _teamMemberService.AddAsync(request, cancellationToken);
        return result.IsSuccess ? CreatedAtAction(nameof(Get), new { result.Value.Id }, result.Value) : result.ToProblem();
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] TeamMembersRequest request, CancellationToken cancellationToken)
    {
        var result = await _teamMemberService.UpdateAsync(id, request, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        var result = await _teamMemberService.DeleteAsync(id, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
