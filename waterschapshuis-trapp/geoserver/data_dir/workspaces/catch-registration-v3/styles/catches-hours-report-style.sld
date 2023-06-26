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
      <FeatureTypeStyle>
        <Rule>
          <Title>0</Title>
          <ogc:Filter>
            <ogc:PropertyIsEqualTo>
              <ogc:PropertyName>CatchNumber</ogc:PropertyName>
              <ogc:Literal>0</ogc:Literal>
            </ogc:PropertyIsEqualTo>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#ffffff</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>

        <Rule>
          <Title>0-5</Title>
          <ogc:Filter>
            <ogc:And>
              <ogc:PropertyIsGreaterThan>
                <ogc:PropertyName>CatchNumber</ogc:PropertyName>
                <ogc:Literal>0</ogc:Literal>
              </ogc:PropertyIsGreaterThan>
              <ogc:PropertyIsLessThanOrEqualTo>
                <ogc:PropertyName>CatchNumber</ogc:PropertyName>
                <ogc:Literal>5</ogc:Literal>
              </ogc:PropertyIsLessThanOrEqualTo>
            </ogc:And>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#cdffcd</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>

        <Rule>
          <Title>5-10</Title>
          <ogc:Filter>
            <ogc:And>
              <ogc:PropertyIsGreaterThan>
                <ogc:PropertyName>CatchNumber</ogc:PropertyName>
                <ogc:Literal>5</ogc:Literal>
              </ogc:PropertyIsGreaterThan>
              <ogc:PropertyIsLessThanOrEqualTo>
                <ogc:PropertyName>CatchNumber</ogc:PropertyName>
                <ogc:Literal>10</ogc:Literal>
              </ogc:PropertyIsLessThanOrEqualTo>
            </ogc:And>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#ffff69</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>

        <Rule>
          <Title>10-15</Title>
          <ogc:Filter>
            <ogc:And>
              <ogc:PropertyIsGreaterThan>
                <ogc:PropertyName>CatchNumber</ogc:PropertyName>
                <ogc:Literal>10</ogc:Literal>
              </ogc:PropertyIsGreaterThan>
              <ogc:PropertyIsLessThanOrEqualTo>
                <ogc:PropertyName>CatchNumber</ogc:PropertyName>
                <ogc:Literal>15</ogc:Literal>
              </ogc:PropertyIsLessThanOrEqualTo>
            </ogc:And>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#ffaf4b</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>

        <Rule>
          <Title>15-20</Title>
          <ogc:Filter>
            <ogc:And>
              <ogc:PropertyIsGreaterThan>
                <ogc:PropertyName>CatchNumber</ogc:PropertyName>
                <ogc:Literal>15</ogc:Literal>
              </ogc:PropertyIsGreaterThan>
              <ogc:PropertyIsLessThanOrEqualTo>
                <ogc:PropertyName>CatchNumber</ogc:PropertyName>
                <ogc:Literal>20</ogc:Literal>
              </ogc:PropertyIsLessThanOrEqualTo>
            </ogc:And>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#ff6630</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>

        <Rule>
          <Title>20-30</Title>
          <ogc:Filter>
            <ogc:And>
              <ogc:PropertyIsGreaterThan>
                <ogc:PropertyName>CatchNumber</ogc:PropertyName>
                <ogc:Literal>20</ogc:Literal>
              </ogc:PropertyIsGreaterThan>
              <ogc:PropertyIsLessThanOrEqualTo>
                <ogc:PropertyName>CatchNumber</ogc:PropertyName>
                <ogc:Literal>30</ogc:Literal>
              </ogc:PropertyIsLessThanOrEqualTo>
            </ogc:And>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#ff0f0f</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>

        <Rule>
          <Title>30-50</Title>
          <ogc:Filter>
            <ogc:And>
              <ogc:PropertyIsGreaterThan>
                <ogc:PropertyName>CatchNumber</ogc:PropertyName>
                <ogc:Literal>30</ogc:Literal>
              </ogc:PropertyIsGreaterThan>
              <ogc:PropertyIsLessThanOrEqualTo>
                <ogc:PropertyName>CatchNumber</ogc:PropertyName>
                <ogc:Literal>50</ogc:Literal>
              </ogc:PropertyIsLessThanOrEqualTo>
            </ogc:And>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#a53700</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>

        <Rule>
          <Title>50-75</Title>
          <ogc:Filter>
            <ogc:And>
              <ogc:PropertyIsGreaterThan>
                <ogc:PropertyName>CatchNumber</ogc:PropertyName>
                <ogc:Literal>50</ogc:Literal>
              </ogc:PropertyIsGreaterThan>
              <ogc:PropertyIsLessThanOrEqualTo>
                <ogc:PropertyName>CatchNumber</ogc:PropertyName>
                <ogc:Literal>75</ogc:Literal>
              </ogc:PropertyIsLessThanOrEqualTo>
            </ogc:And>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#870a5a</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>

        <Rule>
          <Title>75-150</Title>
          <ogc:Filter>
            <ogc:And>
              <ogc:PropertyIsGreaterThan>
                <ogc:PropertyName>CatchNumber</ogc:PropertyName>
                <ogc:Literal>75</ogc:Literal>
              </ogc:PropertyIsGreaterThan>
              <ogc:PropertyIsLessThanOrEqualTo>
                <ogc:PropertyName>CatchNumber</ogc:PropertyName>
                <ogc:Literal>150</ogc:Literal>
              </ogc:PropertyIsLessThanOrEqualTo>
            </ogc:And>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#50008c</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>

        <Rule>
          <Title>150-200</Title>
          <ogc:Filter>
            <ogc:And>
              <ogc:PropertyIsGreaterThan>
                <ogc:PropertyName>CatchNumber</ogc:PropertyName>
                <ogc:Literal>150</ogc:Literal>
              </ogc:PropertyIsGreaterThan>
              <ogc:PropertyIsLessThanOrEqualTo>
                <ogc:PropertyName>CatchNumber</ogc:PropertyName>
                <ogc:Literal>200</ogc:Literal>
              </ogc:PropertyIsLessThanOrEqualTo>
            </ogc:And>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#000000</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>

        <Rule>
          <Title>200</Title>
          <ogc:Filter>
            <ogc:PropertyIsGreaterThan>
              <ogc:PropertyName>CatchNumber</ogc:PropertyName>
              <ogc:Literal>200</ogc:Literal>
            </ogc:PropertyIsGreaterThan>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <GraphicFill>
                <Graphic>
                  <Mark>
                    <WellKnownName>shape://times</WellKnownName>
                    <Stroke>
                      <CssParameter name="stroke">#000000</CssParameter>
                      <CssParameter name="stroke-width">1</CssParameter>
                    </Stroke>
                  </Mark>
                  <Size>5</Size>
                </Graphic>
              </GraphicFill>
            </Fill>
          </PolygonSymbolizer>
        </Rule>

        <Rule>
          <Title>0.00</Title>
          <ogc:Filter>
            <ogc:PropertyIsEqualTo>
              <ogc:PropertyName>CatchesPerKM</ogc:PropertyName>
              <ogc:Literal>0.00</ogc:Literal>
            </ogc:PropertyIsEqualTo>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#ffffff</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>

        <Rule>
          <Title>0.00-0.01</Title>
          <ogc:Filter>
            <ogc:And>
              <ogc:PropertyIsGreaterThan>
                <ogc:PropertyName>CatchesPerKM</ogc:PropertyName>
                <ogc:Literal>0.00</ogc:Literal>
              </ogc:PropertyIsGreaterThan>
              <ogc:PropertyIsLessThanOrEqualTo>
                <ogc:PropertyName>CatchesPerKM</ogc:PropertyName>
                <ogc:Literal>0.01</ogc:Literal>
              </ogc:PropertyIsLessThanOrEqualTo>
            </ogc:And>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#cdffcd</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>

        <Rule>
          <Title>0.01-0.05</Title>
          <ogc:Filter>
            <ogc:And>
              <ogc:PropertyIsGreaterThan>
                <ogc:PropertyName>CatchesPerKM</ogc:PropertyName>
                <ogc:Literal>0.01</ogc:Literal>
              </ogc:PropertyIsGreaterThan>
              <ogc:PropertyIsLessThanOrEqualTo>
                <ogc:PropertyName>CatchesPerKM</ogc:PropertyName>
                <ogc:Literal>0.05</ogc:Literal>
              </ogc:PropertyIsLessThanOrEqualTo>
            </ogc:And>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#ffff69</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>

        <Rule>
          <Title>0.05-0.10</Title>
          <ogc:Filter>
            <ogc:And>
              <ogc:PropertyIsGreaterThan>
                <ogc:PropertyName>CatchesPerKM</ogc:PropertyName>
                <ogc:Literal>0.05</ogc:Literal>
              </ogc:PropertyIsGreaterThan>
              <ogc:PropertyIsLessThanOrEqualTo>
                <ogc:PropertyName>CatchesPerKM</ogc:PropertyName>
                <ogc:Literal>0.10</ogc:Literal>
              </ogc:PropertyIsLessThanOrEqualTo>
            </ogc:And>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#ffaf4b</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>

        <Rule>
          <Title>0.10-0.15</Title>
          <ogc:Filter>
            <ogc:And>
              <ogc:PropertyIsGreaterThan>
                <ogc:PropertyName>CatchesPerKM</ogc:PropertyName>
                <ogc:Literal>0.10</ogc:Literal>
              </ogc:PropertyIsGreaterThan>
              <ogc:PropertyIsLessThanOrEqualTo>
                <ogc:PropertyName>CatchesPerKM</ogc:PropertyName>
                <ogc:Literal>0.15</ogc:Literal>
              </ogc:PropertyIsLessThanOrEqualTo>
            </ogc:And>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#ff6630</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>

        <Rule>
          <Title>0.15-0.20</Title>
          <ogc:Filter>
            <ogc:And>
              <ogc:PropertyIsGreaterThan>
                <ogc:PropertyName>CatchesPerKM</ogc:PropertyName>
                <ogc:Literal>0.15</ogc:Literal>
              </ogc:PropertyIsGreaterThan>
              <ogc:PropertyIsLessThanOrEqualTo>
                <ogc:PropertyName>CatchesPerKM</ogc:PropertyName>
                <ogc:Literal>0.20</ogc:Literal>
              </ogc:PropertyIsLessThanOrEqualTo>
            </ogc:And>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#ff0f0f</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>

        <Rule>
          <Title>0.20-0.30</Title>
          <ogc:Filter>
            <ogc:And>
              <ogc:PropertyIsGreaterThan>
                <ogc:PropertyName>CatchesPerKM</ogc:PropertyName>
                <ogc:Literal>0.20</ogc:Literal>
              </ogc:PropertyIsGreaterThan>
              <ogc:PropertyIsLessThanOrEqualTo>
                <ogc:PropertyName>CatchesPerKM</ogc:PropertyName>
                <ogc:Literal>0.30</ogc:Literal>
              </ogc:PropertyIsLessThanOrEqualTo>
            </ogc:And>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#a53700</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>

        <Rule>
          <Title>0.30-0.50</Title>
          <ogc:Filter>
            <ogc:And>
              <ogc:PropertyIsGreaterThan>
                <ogc:PropertyName>CatchesPerKM</ogc:PropertyName>
                <ogc:Literal>0.30</ogc:Literal>
              </ogc:PropertyIsGreaterThan>
              <ogc:PropertyIsLessThanOrEqualTo>
                <ogc:PropertyName>CatchesPerKM</ogc:PropertyName>
                <ogc:Literal>0.50</ogc:Literal>
              </ogc:PropertyIsLessThanOrEqualTo>
            </ogc:And>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#870a5a</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>

        <Rule>
          <Title>0.50-0.75</Title>
          <ogc:Filter>
            <ogc:And>
              <ogc:PropertyIsGreaterThan>
                <ogc:PropertyName>CatchesPerKM</ogc:PropertyName>
                <ogc:Literal>0.50</ogc:Literal>
              </ogc:PropertyIsGreaterThan>
              <ogc:PropertyIsLessThanOrEqualTo>
                <ogc:PropertyName>CatchesPerKM</ogc:PropertyName>
                <ogc:Literal>0.75</ogc:Literal>
              </ogc:PropertyIsLessThanOrEqualTo>
            </ogc:And>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#50008c</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>

        <Rule>
          <Title>0.75-1.5</Title>
          <ogc:Filter>
            <ogc:And>
              <ogc:PropertyIsGreaterThan>
                <ogc:PropertyName>CatchesPerKM</ogc:PropertyName>
                <ogc:Literal>0.75</ogc:Literal>
              </ogc:PropertyIsGreaterThan>
              <ogc:PropertyIsLessThanOrEqualTo>
                <ogc:PropertyName>CatchesPerKM</ogc:PropertyName>
                <ogc:Literal>1.5</ogc:Literal>
              </ogc:PropertyIsLessThanOrEqualTo>
            </ogc:And>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#000000</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>

        <Rule>
          <Title>1.5</Title>
          <ogc:Filter>
            <ogc:PropertyIsGreaterThan>
              <ogc:PropertyName>CatchesPerKM</ogc:PropertyName>
              <ogc:Literal>1.5</ogc:Literal>
            </ogc:PropertyIsGreaterThan>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <GraphicFill>
                <Graphic>
                  <Mark>
                    <WellKnownName>shape://times</WellKnownName>
                    <Stroke>
                      <CssParameter name="stroke">#000000</CssParameter>
                      <CssParameter name="stroke-width">1</CssParameter>
                    </Stroke>
                  </Mark>
                  <Size>5</Size>
                </Graphic>
              </GraphicFill>
            </Fill>
          </PolygonSymbolizer>
        </Rule>


        <Rule>
          <Title>0.00</Title>
          <ogc:Filter>
            <ogc:PropertyIsEqualTo>
              <ogc:PropertyName>HoursPerKM</ogc:PropertyName>
              <ogc:Literal>0.00</ogc:Literal>
            </ogc:PropertyIsEqualTo>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#ffffff</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>

        <Rule>
          <Title>0.00-0.25</Title>
          <ogc:Filter>
            <ogc:And>
              <ogc:PropertyIsGreaterThan>
                <ogc:PropertyName>HoursPerKM</ogc:PropertyName>
                <ogc:Literal>0.00</ogc:Literal>
              </ogc:PropertyIsGreaterThan>
              <ogc:PropertyIsLessThanOrEqualTo>
                <ogc:PropertyName>HoursPerKM</ogc:PropertyName>
                <ogc:Literal>0.25</ogc:Literal>
              </ogc:PropertyIsLessThanOrEqualTo>
            </ogc:And>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#cdffcd</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>

        <Rule>
          <Title>0.25-0.50</Title>
          <ogc:Filter>
            <ogc:And>
              <ogc:PropertyIsGreaterThan>
                <ogc:PropertyName>HoursPerKM</ogc:PropertyName>
                <ogc:Literal>0.25</ogc:Literal>
              </ogc:PropertyIsGreaterThan>
              <ogc:PropertyIsLessThanOrEqualTo>
                <ogc:PropertyName>HoursPerKM</ogc:PropertyName>
                <ogc:Literal>0.50</ogc:Literal>
              </ogc:PropertyIsLessThanOrEqualTo>
            </ogc:And>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#ffff69</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>

        <Rule>
          <Title>0.50-0.75</Title>
          <ogc:Filter>
            <ogc:And>
              <ogc:PropertyIsGreaterThan>
                <ogc:PropertyName>HoursPerKM</ogc:PropertyName>
                <ogc:Literal>0.50</ogc:Literal>
              </ogc:PropertyIsGreaterThan>
              <ogc:PropertyIsLessThanOrEqualTo>
                <ogc:PropertyName>HoursPerKM</ogc:PropertyName>
                <ogc:Literal>0.75</ogc:Literal>
              </ogc:PropertyIsLessThanOrEqualTo>
            </ogc:And>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#ffaf4b</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>

        <Rule>
          <Title>0.75-1.00</Title>
          <ogc:Filter>
            <ogc:And>
              <ogc:PropertyIsGreaterThan>
                <ogc:PropertyName>HoursPerKM</ogc:PropertyName>
                <ogc:Literal>0.75</ogc:Literal>
              </ogc:PropertyIsGreaterThan>
              <ogc:PropertyIsLessThanOrEqualTo>
                <ogc:PropertyName>HoursPerKM</ogc:PropertyName>
                <ogc:Literal>1.00</ogc:Literal>
              </ogc:PropertyIsLessThanOrEqualTo>
            </ogc:And>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#ff6630</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>

        <Rule>
          <Title>1.00-1.50</Title>
          <ogc:Filter>
            <ogc:And>
              <ogc:PropertyIsGreaterThan>
                <ogc:PropertyName>HoursPerKM</ogc:PropertyName>
                <ogc:Literal>1.00</ogc:Literal>
              </ogc:PropertyIsGreaterThan>
              <ogc:PropertyIsLessThanOrEqualTo>
                <ogc:PropertyName>HoursPerKM</ogc:PropertyName>
                <ogc:Literal>1.50</ogc:Literal>
              </ogc:PropertyIsLessThanOrEqualTo>
            </ogc:And>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#ff0f0f</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>

        <Rule>
          <Title>1.50-2.00</Title>
          <ogc:Filter>
            <ogc:And>
              <ogc:PropertyIsGreaterThan>
                <ogc:PropertyName>HoursPerKM</ogc:PropertyName>
                <ogc:Literal>1.50</ogc:Literal>
              </ogc:PropertyIsGreaterThan>
              <ogc:PropertyIsLessThanOrEqualTo>
                <ogc:PropertyName>HoursPerKM</ogc:PropertyName>
                <ogc:Literal>2.00</ogc:Literal>
              </ogc:PropertyIsLessThanOrEqualTo>
            </ogc:And>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#a53700</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>

        <Rule>
          <Title>2.00-3.00</Title>
          <ogc:Filter>
            <ogc:And>
              <ogc:PropertyIsGreaterThan>
                <ogc:PropertyName>HoursPerKM</ogc:PropertyName>
                <ogc:Literal>2.00</ogc:Literal>
              </ogc:PropertyIsGreaterThan>
              <ogc:PropertyIsLessThanOrEqualTo>
                <ogc:PropertyName>HoursPerKM</ogc:PropertyName>
                <ogc:Literal>3.00</ogc:Literal>
              </ogc:PropertyIsLessThanOrEqualTo>
            </ogc:And>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#870a5a</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>

        <Rule>
          <Title>3.00-4.00</Title>
          <ogc:Filter>
            <ogc:And>
              <ogc:PropertyIsGreaterThan>
                <ogc:PropertyName>HoursPerKM</ogc:PropertyName>
                <ogc:Literal>3.00</ogc:Literal>
              </ogc:PropertyIsGreaterThan>
              <ogc:PropertyIsLessThanOrEqualTo>
                <ogc:PropertyName>HoursPerKM</ogc:PropertyName>
                <ogc:Literal>4.00</ogc:Literal>
              </ogc:PropertyIsLessThanOrEqualTo>
            </ogc:And>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#50008c</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>

        <Rule>
          <Title>4.00-5.00</Title>
          <ogc:Filter>
            <ogc:And>
              <ogc:PropertyIsGreaterThan>
                <ogc:PropertyName>HoursPerKM</ogc:PropertyName>
                <ogc:Literal>4.00</ogc:Literal>
              </ogc:PropertyIsGreaterThan>
              <ogc:PropertyIsLessThanOrEqualTo>
                <ogc:PropertyName>HoursPerKM</ogc:PropertyName>
                <ogc:Literal>5.00</ogc:Literal>
              </ogc:PropertyIsLessThanOrEqualTo>
            </ogc:And>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#000000</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>

        <Rule>
          <Title>5.00</Title>
          <ogc:Filter>
            <ogc:PropertyIsGreaterThan>
              <ogc:PropertyName>HoursPerKM</ogc:PropertyName>
              <ogc:Literal>5.00</ogc:Literal>
            </ogc:PropertyIsGreaterThan>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <GraphicFill>
                <Graphic>
                  <Mark>
                    <WellKnownName>shape://times</WellKnownName>
                    <Stroke>
                      <CssParameter name="stroke">#000000</CssParameter>
                      <CssParameter name="stroke-width">1</CssParameter>
                    </Stroke>
                  </Mark>
                  <Size>5</Size>
                </Graphic>
              </GraphicFill>
            </Fill>
          </PolygonSymbolizer>
        </Rule>

      </FeatureTypeStyle>
    </UserStyle>
  </NamedLayer>
</StyledLayerDescriptor>