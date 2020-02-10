using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Keepr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class KeepsController : ControllerBase
  {
    private readonly KeepsService _ks;
    public KeepsController(KeepsService ks)
    {
      _ks = ks;
    }
    [HttpGet]
    public ActionResult<IEnumerable<Keep>> Get()
    {
      try
      {
        return Ok(_ks.Get());
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      };
    }
    [Authorize]
    [HttpGet("user")]
    public ActionResult<IEnumerable<Keep>> GetByUser()
    {
      try
      {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        return Ok(_ks.GetByUser(userId));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{id}")]
    public ActionResult<Keep> GetById(int id)
    {
      try
      {
        string userId;
        try
        {
          userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
        catch
        {
          userId = " ";
        }
        return Ok(_ks.GetById(id, userId));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpPost]
    [Authorize]
    public ActionResult<Keep> Post([FromBody] Keep newKeep)
    {
      try
      {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        newKeep.UserId = userId;
        return Ok(_ks.Create(newKeep));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpPut("{id}")]
    [Authorize]
    public ActionResult<Keep> Edit([FromBody]Keep Update, int id)
    {
      try
      {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        Update.UserId = userId;
        Update.Id = id;
        return _ks.Edit(Update);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpPut("{id}/view")]
    public ActionResult<int> ViewKeep(int id)
    {
      try
      {
        return _ks.ViewKeep(id);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpPut("{id}/keep")]
    [Authorize]
    public ActionResult<int> KeepKeep(int id)
    {
      try
      {
        return _ks.KeepKeep(id);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpDelete("{id}")]
    [Authorize]
    public ActionResult<String> Delete(int id)
    {
      try
      {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        _ks.Delete(id, userId);
        return Ok("deleted");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

  }
}