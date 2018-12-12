using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiHost.Models;
using ApiHost.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiHost.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class GrayScaleController : Controller
    {
        IDataAccess<GrayScaleWebsite, int> _dataAccess;
        public GrayScaleController(IDataAccess<GrayScaleWebsite, int> dataAccess)
        {
            _dataAccess = dataAccess;
        }
        [HttpGet("GetData/{id}", Name = "GetData")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public IActionResult Create([FromBody] GrayScaleWebsite item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _dataAccess.AddWebsite(item);
            return Ok();
        }

        [HttpGet("Website/{id}", Name = "GetWebsite")]
        public IActionResult GetById(int id)
        {
            var item = _dataAccess.GetWebsite(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPut]
        public IActionResult Update([FromBody] GrayScaleWebsite item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _dataAccess.UpdateWebsite(item.Id,item);
            return Ok();
        }
    }
}