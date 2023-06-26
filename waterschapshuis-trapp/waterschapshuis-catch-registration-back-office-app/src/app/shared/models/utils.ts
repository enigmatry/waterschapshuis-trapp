export function groupByToKeyValue(xs: Array<any>, key: string): Array<any> {
    const groupedObjects = xs.reduce((rv, x) => {
      (rv[x[key]] = rv[x[key]] || []).push(x);
      return rv;
    }, {});

    const result: Array<any> = [];
    Object.keys(groupedObjects).forEach((objectName) => result.push({ key: objectName, items: Reflect.get(groupedObjects, objectName)}));

    return result;
  }

export function distinct(value: any, index: any, self: any) {
  return self.indexOf(value) === index;
}

export function dateAsDayMontYearString(request: Date | null = null): string {
  const date = request == null ? new Date() : request;
  return `${date.getDate()}-${date.getMonth() + 1}-${date.getFullYear()}`;
}

export function dateAsYearMonthDay(date: Date): string {
  const year = date.getFullYear().toString().padStart(4, '0');
  const month = (date.getMonth() + 1).toString().padStart(2, '0');
  const day = date.getDate().toString().padStart(2, '0');
  return `${year}-${month}-${day}`;
}

export function isString(value: any) {
  return typeof value === 'string' || value instanceof String;
}
