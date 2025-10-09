using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.API.Controllers;

[Authorize(Roles = "Admin")]
[Route("api/[controller]")]
public class BranchController : BaseController
{
    
}