using DbCore.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using DbManagerService;
using DbCore.Dto;

namespace DbCore.Controllers
{
    public class DbController(IContextInteractor contextInteractor, IDbManager dbManager, IValidator validator) : Controller
    {
        [HttpPost("Database")]
        public IActionResult CreateDatabase(string dbName)
        {
            try
            {
                if (!validator.ValidDbName(dbName, out string notValidReason))
                {
                    return BadRequest(notValidReason);
                }

                dbManager.CreateDatabase(dbName);
                contextInteractor.CreateDataBase(dbName);
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorTemplate(ex.Message, ex.StackTrace);
                return StatusCode(500, errorResponse.Serialize());
            }

            return Ok();
        }
    }
}
