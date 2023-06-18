namespace Adaptations.Web.ViewModels.Settings
{
    using Adaptations.Data.Models;
    using Adaptations.Services.Mapping;

    public class SettingViewModel : IMapFrom<Setting>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}
