using DbCore.Dto;
using DbCore.Services;
using DbManagerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Xml.Linq;

namespace DbCore.Controllers
{
    public class TableController(IContextInteractor contextInteractor, IDbManager dbManager, IValidator validator) : Controller
    {
        [HttpPost("{database}/Table")]
        public IActionResult CreateTable(string database, [FromBody] CreateTableBody table)
        {
            try
            {
                if (!validator.ValidTable(database, table.Name, table.Columns, out string notValidReason))
                {
                    return BadRequest(notValidReason);
                }

                dbManager.CreateTable(database, table.Name, table.Columns);
                contextInteractor.CreateTable(database, table.Name, table.Columns);
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorTemplate(ex.Message, ex.StackTrace);
                return StatusCode(500, errorResponse.Serialize());
            }
            return Ok(database);
        }
    }
}
