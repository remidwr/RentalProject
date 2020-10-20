﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using DAL.Models;
using ProjetLocation.API.Models.User.RoleName;
using Microsoft.AspNetCore.Authorization;
using ProjetLocation.API.Services;
using ProjetLocation.API.Models.Good;

namespace ProjetLocation.API.Controllers
{
    [Authorize(Roles = Roles.User + "," + Roles.Admin + "," + Roles.SuperAdmin)]
    [Route("api/[controller]")]
    [ApiController]
    public class SectionController : ControllerBase
    {
        private SectionService _sectionService;

        public SectionController(SectionService sectionService)
        {
            _sectionService = sectionService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<SectionWithCategories> sections = _sectionService.GetAll();

            if (!(sections is null))
                return Ok(sections);
            else
                return Problem(statusCode: (int)HttpStatusCode.NoContent);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Section section = _sectionService.GetById(id);

            if (!(section is null))
                return Ok(section);
            else
                return Problem(statusCode: (int)HttpStatusCode.NoContent);
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.SuperAdmin)]
        [HttpPost]
        public IActionResult Post([FromBody] Section section)
        {
            try
            {
                _sectionService.Post(section);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("UK_Section_Name"))
                    return Problem(detail: "Le nom de cette section existe déjà.",
                                   statusCode: (int)HttpStatusCode.PreconditionFailed);
                else
                    return Problem(detail: "Impossible de créer cette section.",
                                   statusCode: (int)HttpStatusCode.PreconditionFailed);
            }

            return Ok();
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.SuperAdmin)]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Section section)
        {
            try
            {
                _sectionService.Put(id, section);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("UK_Section_Name"))
                    return Problem(detail: "Le nom de cette section existe déjà.",
                                   statusCode: (int)HttpStatusCode.PreconditionFailed);
                else
                    return Problem(detail: "Impossible de mettre à jour cette section.",
                                   statusCode: (int)HttpStatusCode.PreconditionFailed);
            }

            return Ok();
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.SuperAdmin)]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _sectionService.Delete(id);
            }
            catch (Exception)
            {
                return Problem(detail: "Impossible de supprimer cette section.",
                               statusCode: (int)HttpStatusCode.PreconditionFailed);
            }

            return Ok();
        }
    }
}
