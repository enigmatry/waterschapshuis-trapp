export class PredictionTableModel {
    cells: PredictionTableCell[];
    accuracy: number;
}

export class PredictionTableCell {
    constructor(
        public title: string,
        public winter: number,
        public spring: number,
        public summer: number,
        public autumn: number) {}
}

