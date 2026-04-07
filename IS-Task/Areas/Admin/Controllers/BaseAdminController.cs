using IS_Task.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IS_Task.Areas.Admin.Controllers
{
    [Area(DataConstants.AdminConstant)]
    [Authorize(Roles = DataConstants.AdminConstant)]
    public class BaseAdminController : Controller
    {
    }
}
