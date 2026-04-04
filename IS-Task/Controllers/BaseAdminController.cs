using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IS_Task.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BaseAdminController : Controller
    {
    }
}
