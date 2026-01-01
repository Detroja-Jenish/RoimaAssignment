using API.Entities.General;
using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers;

[ApiController]
[Route("api/interview-rounds")]
public class InterviewRoundController : ControllerBase
{
    private readonly IInterviewRoundRepository _repository;

    public InterviewRoundController(IInterviewRoundRepository repository)
    {
        _repository = repository;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateInterviewRoundDto dto)
    {
        var round = await _repository.CreateAsync(dto);
        return Ok(round);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateInterviewRoundDto dto)
    {
        var round = await _repository.UpdateAsync(id, dto);
        if (round == null)
            return NotFound();

        return Ok(round);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var round = await _repository.GetByIdAsync(id);
        if (round == null)
            return NotFound();

        return Ok(round);
    }

    [HttpGet("job-opening/{jobOpeningId}")]
    public async Task<IActionResult> GetByJobOpening(int jobOpeningId)
    {
        var rounds = await _repository.GetByJobOpeningAsync(jobOpeningId);
        return Ok(rounds);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _repository.DeleteAsync(id);
        if (!deleted)
            return NotFound();

        return Ok(new { Message = "Interview round deleted" });
    }
}
