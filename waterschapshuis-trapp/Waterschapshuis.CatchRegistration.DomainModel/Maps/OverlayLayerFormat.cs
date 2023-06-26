using System.ComponentModel.DataAnnotations;

namespace Waterschapshuis.CatchRegistration.DomainModel.Maps
{
    public enum OverlayLayerFormat
    {
        [Display(Name = "image/png")]
        Image = 1,
        [Display(Name = "application/json")]
        Json = 2
    }
}
