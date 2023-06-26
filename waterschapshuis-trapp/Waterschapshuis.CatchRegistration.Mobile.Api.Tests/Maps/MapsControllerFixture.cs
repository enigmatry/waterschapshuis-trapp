using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Waterschapshuis.CatchRegistration.ApplicationServices.Common;
using Waterschapshuis.CatchRegistration.ApplicationServices.Maps.Layers;
using Waterschapshuis.CatchRegistration.Common.Tests.Api;
using Waterschapshuis.CatchRegistration.DomainModel.Maps;
using Waterschapshuis.CatchRegistration.DomainModel.Maps.Styles;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;
using Waterschapshuis.CatchRegistration.Mobile.Api.Features.Latest.Maps;

namespace Waterschapshuis.CatchRegistration.Mobile.Api.Tests.Maps
{
    [Category("integration")]
    public class MapsControllerFixture : MobileApiIntegrationFixtureBase
    {
        [Test]
        public async Task TestGetBackgroundLayers()
        {
            var response =
                (await Client.GetAsync<ListResponse<GetBackgroundLayers.ResponseItem>>("maps/background-layers")).Items.ToList();

            //Assert
            response.Count.Should().Be(4);

            var defaultBackgroundMap = response.First();
            defaultBackgroundMap.Id.Should().Be("brtachtergrondkaart");
            defaultBackgroundMap.Name.Should().Be("Top10 NL");
            defaultBackgroundMap.Url.Should()
                .Be("https://geodata.nationaalgeoregister.nl/tiles/service/wmts?&request=GetCapabilities&service=WMTS");
            defaultBackgroundMap.ServiceType.Should().Be(MapServiceType.Wmts);
        }

        [Test]
        public async Task TestGetMapLayers()
        {
            var uriParameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("OrderedLayerCategoryCodes",
                    OverlayLayerCategoryCode.MapAreas.ToString()),
                new KeyValuePair<string, string>("OrderedLayerCategoryCodes",
                    OverlayLayerCategoryCode.MapLocations.ToString())
            };

            var response = (await Client.GetAsync<ListResponse<GetOverlayLayers.ResponseItem>>(
                    new Uri("maps/overlay-layers", UriKind.Relative),
                    uriParameters.ToArray())
                ).Items.ToList();

            //Assert
            response.Count.Should().Be(20);
            response.Count(i => i.CategoryCode == OverlayLayerCategoryCode.MapAreas).Should().Be(7);
            response.Count(i => i.CategoryCode == OverlayLayerCategoryCode.MapLocations).Should().Be(13);

            var hourSquareLayerName = $"{LayerConstants.OverlayLayerName.HourSquares}";
            var hourSquareLayerFullName =
                $"{LayerConstants.WorkspaceName.V3}:{LayerConstants.OverlayLayerName.HourSquares}";
            var hourSquareLayer = response.First(x => x.FullName == hourSquareLayerFullName);
            hourSquareLayer.Name.Should().Be(hourSquareLayerName);
            hourSquareLayer.FullName.Should().Be(hourSquareLayerFullName);
            hourSquareLayer.DisplayName.Should().Be("Uurhokken");
            hourSquareLayer.CategoryDisplayName.Should().Be("GEBIEDEN");
            hourSquareLayer.Url.Should()
                .Be(
                    $"https://someGeoserverDomain/geoserver/catch-registration-v3/wfs?typeName={hourSquareLayerFullName}&outputFormat=application/json&request=GetFeature");
            hourSquareLayer.DisplayZIndex.Should().Be(1);
        }

        [Test]
        public async Task TestGetMapStyles()
        {
            //act
            var styles = (await Client.GetAsync<GetMapStyles.Response>("maps/styles")).Items.ToList();

            //assert
            styles.Count.Should().BeGreaterThan(0);

            //one of the trap type styles
            AssertStyle(find(MapStyleLookupKeyCode.TrapType, TrapStatus.Catching, new Guid("a0a0503e-0cd7-0642-73ab-464e7ca0a28e")),
                "trap-conibear.svg");

            MapStyleLookup find(MapStyleLookupKeyCode code, TrapStatus? status, Guid? trapTypeId = null)
            {
                return styles.FirstOrDefault(r =>
                    r.Key.LookupKeyCode == code
                    && (!status.HasValue || r.Key.TrapStatus == status)
                    && (!trapTypeId.HasValue || r.Key.TrapTypeId == trapTypeId));
            }
        }

        private static void AssertStyle(MapStyleLookup style,
            string iconName)
        {
            style.Should().NotBeNull();
            style.IconName.Should().Be(iconName);
        }
    }
}
