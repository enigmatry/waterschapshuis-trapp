export abstract class ReportTemplateBase {
  constructor(
    public title: string,
    public group: string,
    public routeUri: string,
    public exported: boolean
  ) { }

  public createRouterLink = (): string[] => [ '/reports', this.routeUri ];
}
