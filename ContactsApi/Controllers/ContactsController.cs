using Domain.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service;
using Shared.Dtos.Auth.Dtos;
using Shared.Dtos.Contact.Dtos;
using System.Security.Claims;

namespace ContactsApi.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/Contacts")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ContactsController(IContactService contactService, UserManager<ApplicationUser> userManager)
        {
            _contactService = contactService;
            _userManager = userManager;
        }

        // Get all contacts
        [Authorize(Roles ="Admin")]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {

            var contacts = await _contactService.GetAllAsync();
            if (contacts == null)
                return NotFound("You have no Contacts yet!");
            return Ok(contacts);
        }


        // Creating contacts by user
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateContactDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (success,errorMessage) = await _contactService.CreateAsync(dto, GetUserId());
            if (!success)
                return BadRequest(errorMessage);

            return Ok("Contact created successfully");
        }


        // Get all (SortedBy & Paginated) User`s contacts
        [HttpGet("GetUserContacts")]
        public async Task<IActionResult> GetUserContacts(string sortBy = "Id", int page = 1, int pageSize = 5)
        {
            var userId = GetUserId();
            var contacts = await _contactService.GetUserContactsAsync(userId, sortBy, page, pageSize);
            if (contacts == null)
                return NotFound("You have no Contacts yet!");
            return Ok(contacts);
        }


        // Get User`s contact By Id
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var userId = GetUserId();
            var contact = await _contactService.GetByIdAsync(id, userId);
            if (contact == null)
                return NotFound("Contact not found");

            return Ok(contact);
        }


        // Delete User`s contact
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _contactService.DeleteAsync(id, GetUserId());
            if (!success)
                return NotFound("Contact not found or not yours");

            return Ok("Contact deleted successfully");
        } 


        // Get the current loggined (Authenticated) user
        private string GetUserId(){
            var Id = User.FindFirstValue("uid");
            return Id;
        }
    }

}