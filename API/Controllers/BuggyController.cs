using System;
using API.DTOs;
using Core.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BuggyController : BaseApiControllers
{
[HttpGet("unauthorized")]
  public IActionResult GetUnauthorized()
  {
    
    return Unauthorized();
  }

  [HttpGet("badrequest")]
  public IActionResult GetBadRequest()
  {
    
    return BadRequest("Bad request");
  }

  [HttpGet("notfound")]
  public IActionResult GetNotFound()
  {
    
    return NotFound();
  }

  [HttpGet("internalerror")]
  public IActionResult GetInternalError()
  {
    
    throw new Exception("This ia a test example");
  }

[HttpPost("validationerror")]
  public IActionResult GetValidationError(CreateProductDto product)
  {
    
    return Ok();
  }
}
