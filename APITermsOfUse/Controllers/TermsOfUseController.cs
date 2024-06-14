using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APITermsOfUse.Data;
using Models;
using DataAPI.Data;
using APITermsOfUse.Services;
using Models.DTO;

namespace APITermsOfUse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TermsOfUseController : ControllerBase
    {
        private TermsOfUseService _service;

        public TermsOfUseController(TermsOfUseService termsOfUseService)
        {
            _service = termsOfUseService;
        }
        [HttpGet]
        public List<TermsOfUse> Get()
        {
            return _service.GetAllMongo();
        }

        [HttpGet("{id}")]
        public TermsOfUse GetById(int id)
        {
            return _service.GetMongoById(id);
        }

        [HttpPost]
        public TermsOfUse Post(TermsOfUseDTO dto)
        {
            TermsOfUse tos = new TermsOfUse(dto);
            return _service.InsertMongo(tos);
        }
    }
}
