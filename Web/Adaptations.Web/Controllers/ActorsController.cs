namespace Adaptations.Web.Controllers
{
    using Adaptations.Services.Data;
    using Adaptations.Web.ViewModels.Actors;
    using Microsoft.AspNetCore.Mvc;

    public class ActorsController : BaseController
    {
        private readonly IActorsService actorsService;

        public ActorsController(
           IActorsService actorsService)
            {
                this.actorsService = actorsService;
            }

        public IActionResult ActorId(int id)
        {
            var actor = this.actorsService.GetActorById<SingleActorViewModel>(id);

            actor.ShortBio = this.actorsService.BioSummary(id);

            return this.View(actor);
        }
    }
}
