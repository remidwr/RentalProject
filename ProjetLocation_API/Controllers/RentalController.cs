﻿using System;
using System.Collections.Generic;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using ProjetLocation.API.Models.User.RoleName;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using ProjetLocation.API.Models.Rental;
using ProjetLocation.API.Services;

namespace ProjetLocation.API.Controllers
{
    [Authorize(Roles = Roles.User + "," + Roles.Admin + "," + Roles.SuperAdmin)]
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private RentalService _rentalService;

        public RentalController(RentalService rentalService)
        {
            _rentalService = rentalService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<RentalFull> rentals = _rentalService.GetAll();

            if (!(rentals is null))
                return Ok(rentals);
            else
                return Problem(statusCode: (int)HttpStatusCode.NoContent);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            RentalFull rental = _rentalService.GetById(id);

            if (!(rental is null))
                return Ok(rental);
            else
                return Problem(statusCode: (int)HttpStatusCode.NoContent);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Rental rental)
        {
            try
            {
                _rentalService.Post(rental);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("CK_Rental_RentedDate"))
                    return Problem(detail: "La date de fin de location doit être supérieure à la date de début de location.",
                                   statusCode: (int)HttpStatusCode.PreconditionFailed);
                else if (ex.Message.Contains("CK_Rental_Amount"))
                    return Problem(detail: "Le montant doit être positif.",
                                   statusCode: (int)HttpStatusCode.PreconditionFailed);
                else if (ex.Message.Contains("CK_Rental_Deposit"))
                    return Problem(detail: "La caution doit être positive.",
                                   statusCode: (int)HttpStatusCode.PreconditionFailed);
                else
                    return Problem(detail: "Impossible de créer la location.",
                                   statusCode: (int)HttpStatusCode.PreconditionFailed);
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Rental rental)
        {
            try
            {
                _rentalService.Put(id, rental);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("CK_Rental_RentedDate"))
                    return Problem(detail: "La date de fin de location doit être supérieure à la date de début de location.",
                                   statusCode: (int)HttpStatusCode.PreconditionFailed);
                else if (ex.Message.Contains("CK_Rental_Amount"))
                    return Problem(detail: "Le montant doit être positif.",
                                   statusCode: (int)HttpStatusCode.PreconditionFailed);
                else if (ex.Message.Contains("CK_Rental_Deposit"))
                    return Problem(detail: "La caution doit être positive.",
                                   statusCode: (int)HttpStatusCode.PreconditionFailed);
                else
                    return Problem(detail: "Impossible de mettre à jour la location.",
                                   statusCode: (int)HttpStatusCode.PreconditionFailed);
            }

            return Ok();
        }

        [HttpPut("{id}/rating")]
        public IActionResult UpdateRating(int id, [FromBody] RentalRating rental)
        {
            try
            {
                _rentalService.UpdateRating(id, rental);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("CK_Rental_UnableRating"))
                    return Problem(detail: "Impossible d'ajouter une évaluation tant que la location n'est pas finie.",
                                   statusCode: (int)HttpStatusCode.PreconditionFailed);
                else if (ex.Message.Contains("CK_Rental_Rating"))
                    return Problem(detail: "L'évaluation doit être entre 1 et 5.",
                                   statusCode: (int)HttpStatusCode.PreconditionFailed);
                else
                    return Problem(detail: "Impossible d'évaluer la location.",
                                   statusCode: (int)HttpStatusCode.PreconditionFailed);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _rentalService.Delete(id);
            }
            catch (Exception)
            {
                return Problem(detail: "Impossible de supprimer la location.",
                               statusCode: (int)HttpStatusCode.PreconditionFailed);
            }

            return Ok();
        }
    }
}
