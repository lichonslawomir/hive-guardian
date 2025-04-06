using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace HiveGuardian.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DataController : ControllerBase
{
    private readonly AppDbContext _db;
    public DataController(AppDbContext db) => _db = db;

    [HttpGet("latest")]
    public async Task<IActionResult> GetLatest()
    {
        var last = await _db.SensorData.OrderByDescending(d => d.Timestamp).FirstOrDefaultAsync();
        return Ok(last);
    }

    [HttpGet("history")]
    public async Task<IActionResult> GetHistory()
    {
        var data = await _db.SensorData
            .OrderBy(d => d.Timestamp)
            .Take(1000)
            .ToListAsync();

        return Ok(data);
    }
}
