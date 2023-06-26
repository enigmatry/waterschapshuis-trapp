import { get as getProjection } from 'ol/proj';
import { register } from 'ol/proj/proj4';
import Projection from 'ol/proj/Projection';
import proj4 from 'proj4';

export class ProjectionModel {
    private static projection: Projection = null;

    public static dutchMatrix = 'EPSG:28992';
    public static mercatorMatrix = 'EPSG:3857';
    public static geodeticMatrix = 'EPSG:4326';

    public static initDutchProjection(): Projection {
        if (ProjectionModel.projection !== null) {
            return ProjectionModel.projection;
        }
        // these are the coordinates for Dutch projection
        // https://epsg.io/28992
        proj4.defs(
            this.dutchMatrix,
            '+proj=sterea +lat_0=52.15616055555555 +lon_0=5.38763888888889 +k=0.9999079 +x_0=155000 +y_0=463000 +ellps=bessel' +
            ' +towgs84=565.417,50.3319,465.552,-0.398957,0.343988,-1.8774,4.0725 +units=m +no_defs'
        );

        register(proj4);
        const projection = getProjection(this.dutchMatrix);
        projection.setExtent([12628.0541, 308179.0423, 283594.4779, 611063.1429]);
        projection.setWorldExtent([3.2, 50.75, 7.22, 53.7]);
        ProjectionModel.projection = projection;
        return ProjectionModel.projection;
    }
}
