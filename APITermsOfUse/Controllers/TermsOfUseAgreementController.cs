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
    public class AcceptUseTermsController : ControllerBase
    {
        private TermsOfUseAgreementService _service;
        private TermsOfUseService _servicetos;
        private DataAPIContext _context;

        public AcceptUseTermsController(DataAPIContext dataAPIContext, TermsOfUseService termsOfUseService, TermsOfUseAgreementService termsOfUseAgreementService)
        {
            _context = dataAPIContext;
            _service = termsOfUseAgreementService;
            _servicetos = termsOfUseService;
        }

        [HttpGet]
        public List<TermsOfUseAgreement> Get()
        {
            return _service.GetAllMongo();
        }

        [HttpGet("{id}")]
        public TermsOfUseAgreement Get(int id)
        {
            return _service.GetMongoById(id);
        }

        [HttpPost]
        public async Task<ActionResult<TermsOfUseAgreement>> Post(TermsOfUseAgreementDTO term)
        {
            var customer = _context.Customer.Where(x => x.Document == term.CustomerDocument).FirstOrDefault();
            if (customer == null)
                return BadRequest("Cargo não existente");
            var tos = _servicetos.GetMongoById(term.IdTermOfUse);
            if (tos == null)
                return BadRequest("Termos de Uso não existente");

            TermsOfUseAgreement tosa = new TermsOfUseAgreement(term);
            tosa.Customer = customer;
            tosa.TermsOfUse = tos;

            return _service.InsertMongo(tosa);
        }
    }
}
