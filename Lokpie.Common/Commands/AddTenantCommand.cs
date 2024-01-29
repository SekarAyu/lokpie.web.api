using Lokpie.Common.Enums;

namespace Lokpie.Common.Commands
{
    public class AddTenantCommand
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string LogoFileName { get; set; }
        public string Logo { get; set; }
        public RestoEnum Type { get; set; }
    }

    public class UpdateTenantCommand : AddTenantCommand
    {
        public long Id { get; set; }
    }
}
