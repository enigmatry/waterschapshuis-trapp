
export function getDateForWeeksAgoFromToday(numberOfWeeks: number): string {
    const today = new Date();
    const monday = getPreviousMonday(today);
    const date = new Date();
    date.setDate(monday.getDate() - numberOfWeeks * 7);
    return date.toISOString();
}

export function getPreviousMonday(date = null) {
    const prevMonday = date && new Date(date.valueOf()) || new Date();
    prevMonday.setDate(prevMonday.getDate() - (prevMonday.getDay() + 6) % 7);
    return prevMonday;
}
