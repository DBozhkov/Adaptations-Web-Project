namespace Adaptations.Web.Areas.Administration.Controllers
{
    using Adaptations.Common;
    using Adaptations.Web.Controllers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Area("Administration")]
    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class AdministratorController : BaseController
    {
    }
}
