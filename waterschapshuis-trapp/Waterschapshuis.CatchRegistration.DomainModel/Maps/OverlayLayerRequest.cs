using System.ComponentModel.DataAnnotations;

namespace Waterschapshuis.CatchRegistration.DomainModel.Maps
{
    public enum OverlayLayerRequest
    {
        [Display(Name = "GetFeature")]
        Feature = 1,
        [Display(Name = "GetTile")]
        Tile = 2,
        [Display(Name = "GetMap")]
        Image = 3
    }
}
