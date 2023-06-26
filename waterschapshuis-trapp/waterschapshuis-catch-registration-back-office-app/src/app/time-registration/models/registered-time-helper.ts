export class TotalTimeHelper {
  static parseHours(value: string): number {
    const values = value.split(':');
    return Number.parseInt(values[0], 10);
  }

  static parseMinutes(value: string): number {
    const values = value.split(':');
    return Number.parseInt(values[1], 10);
  }

  static toDisplayString(minutes: number, hours?: number): string {
    if (hours !== undefined) {
      return `${String(hours).padStart(2, '0')}:${String(minutes).padStart(2, '0')}`;
    } else {
      hours = Math.floor(minutes / 60);
      minutes = hours === 0 ? minutes : minutes % (hours * 60);
      return hours === 0 && minutes === 0 ? '00:00' : `${String(hours).padStart(2, '0')}:${String(minutes).padStart(2, '0')}`;
    }
  }

  static itemsContainTime(items: any[]) {
    if (!items) {
      return false;
    }
    return !items.every(item => item.time === null);
  }

  static calculateTotalTimeForRow(items: any[]) {
    const totalMinutes = items.map(item => {
      if (!item.time) {
        return 0;
      }
      return TotalTimeHelper.parseHours(item.time) * 60 + TotalTimeHelper.parseMinutes(item.time);
    }).reduce((prev, curr) => prev + curr, 0);

    return totalMinutes;
  }
}
