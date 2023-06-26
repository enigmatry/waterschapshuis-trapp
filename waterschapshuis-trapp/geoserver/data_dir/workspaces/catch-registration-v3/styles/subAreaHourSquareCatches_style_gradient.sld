<?xml version="1.0" encoding="ISO-8859-1"?>
<StyledLayerDescriptor version="1.0.0" 
                       xsi:schemaLocation="http://www.opengis.net/sld StyledLayerDescriptor.xsd" 
                       xmlns="http://www.opengis.net/sld" 
                       xmlns:ogc="http://www.opengis.net/ogc" 
                       xmlns:xlink="http://www.w3.org/1999/xlink" 
                       xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <NamedLayer>
    <Name>Attribute-based polygon</Name>
    <UserStyle>
      <Title>SLD Cook Book: Attribute-based polygon</Title>
      <FeatureTypeStyle>
        <Rule>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">
                <ogc:Function name="Interpolate">
                  <!-- Property to transform -->
                  <ogc:PropertyName>CatchNumber</ogc:PropertyName>

                  <!-- Mapping curve definition pairs (input, output) -->
                  <ogc:Literal>0</ogc:Literal>
                  <ogc:Literal>#000cfa</ogc:Literal>

                  <ogc:Literal>40</ogc:Literal>
                  <ogc:Literal>#73006b</ogc:Literal>

                  <ogc:Literal>150</ogc:Literal>
                  <ogc:Literal>#de1616</ogc:Literal>

                  <!-- Interpolation method -->
                  <ogc:Literal>color</ogc:Literal>

                  <!-- Interpolation mode - defaults to linear -->
                </ogc:Function>
              </CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>
      </FeatureTypeStyle>
    </UserStyle>
  </NamedLayer>
</StyledLayerDescriptor>