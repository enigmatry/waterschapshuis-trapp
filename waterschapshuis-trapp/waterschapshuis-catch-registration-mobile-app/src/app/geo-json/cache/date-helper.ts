/**
 * Adds time to a date. Modelled after MySQL DATE_ADD function.
 * Example: dateAdd(new Date(), 'minute', 30)  //returns 30 minutes from now.
 *
 * @param date  Date to start with
 * @param unit  One of: year, month, week, day, hour, minute, second
 * @param interval  Interval to add
 */
export function dateAdd(date: Date, unit: 'year' | 'month' | 'week' | 'day' | 'hour' | 'minute' | 'second', interval: number) {
    if (!(date instanceof Date)) {
        return undefined;
    }
    let ret = new Date(date);
    const checkRollover = () => { if (ret.getDate() !== date.getDate()) { ret.setDate(0); } };
    switch (String(unit).toLowerCase()) {
        case 'year': ret.setFullYear(ret.getFullYear() + interval); checkRollover(); break;
        case 'month': ret.setMonth(ret.getMonth() + interval); checkRollover(); break;
        case 'week': ret.setDate(ret.getDate() + 7 * interval); break;
        case 'day': ret.setDate(ret.getDate() + interval); break;
        case 'hour': ret.setTime(ret.getTime() + interval * 3600000); break;
        case 'minute': ret.setTime(ret.getTime() + interval * 60000); break;
        case 'second': ret.setTime(ret.getTime() + interval * 1000); break;
        default: ret = undefined; break;
    }
    return ret;
}
