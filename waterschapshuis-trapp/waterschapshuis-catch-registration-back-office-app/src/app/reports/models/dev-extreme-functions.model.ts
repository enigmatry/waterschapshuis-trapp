enum DxFunctionName {
    Divide = 'Divide',
    RelativeComparison = 'RelativeComparison',
    Total = 'Total',
    DivideByTotal = 'DivideByTotal',
    DoNothing = 'DoNothing'
}

class DxFunctionParameters {
    functionName: string;
    parameterNames: string[];

    constructor(functionPlaceholder: string) {
        const parts = functionPlaceholder.split(':');
        this.functionName = parts[0];
        this.parameterNames = parts[1].split(',');
    }
}

export class DxFunctions {
    private static functionsMap = [
        {
            functionName: DxFunctionName.Divide,
            function: (parameters: DxFunctionParameters): (e: any) => any =>
                (e: any): any => {
                    const dividend = e.value(parameters.parameterNames[0]);
                    const divisor = e.value(parameters.parameterNames[1]);
                    return (dividend && divisor) ? dividend / divisor : undefined;
            }
        },
        {
            functionName: DxFunctionName.RelativeComparison,
            function: (parameters: DxFunctionParameters): (e: any) => any =>
                (e: any): any => {
                    const totalAmount = e.value(parameters.parameterNames[0]);
                    const reducingAmount = e.value(parameters.parameterNames[1]);
                    return (totalAmount && reducingAmount) ? ((totalAmount - reducingAmount) / reducingAmount) : undefined;
            }
        },
        {
            functionName: DxFunctionName.Total,
            function: (parameters: DxFunctionParameters): (e: any) => any =>
                (e: any): any => {
                    const total = e.grandTotal(parameters.parameterNames[0]).value(parameters.parameterNames[0]);
                    return (total) ?  total : undefined;
            }
        },
        {
            functionName: DxFunctionName.DivideByTotal,
            function: (parameters: DxFunctionParameters): (e: any) => any =>
                (e: any): any => {
                    const dividend = e.value(parameters.parameterNames[0]);
                    const divisor = e.grandTotal(parameters.parameterNames[1]).value(parameters.parameterNames[1]);
                    return (dividend && divisor) ? dividend / divisor : undefined;
            }
        },
        {
            /// used for preventing default zeroes showing up instead of empty value
            functionName: DxFunctionName.DoNothing,
            function: (parameters: DxFunctionParameters): (e: any) => any =>
                (e: any): any => {
                    const field = e.value(parameters.parameterNames[0]);
                    return (field ) ? field : undefined;
            }
        }
    ];

    static clearCalculateSummaryValues(fields: any[]): string {
        fields
            .filter(x => x.dxFunctionPlaceholder)
            .forEach(field => field.calculateSummaryValue = undefined);
        return JSON.stringify(fields);
    }

    static tryGetFunctionImplementation = (functionPlaceholder: string): any => {
        const parameters = new DxFunctionParameters(functionPlaceholder);
        const functionImplementation = DxFunctions.functionsMap
            .find(x => x.functionName === parameters.functionName)
            .function(parameters);
        if (functionImplementation) {
            return functionImplementation;
        }
        throw new Error(`Could not resolve function implementation for ${functionPlaceholder}!`);
    }

    static sortDescending = (a: any, b: any): number => {
        return (b.value || Number.MAX_VALUE) - (a.value || Number.MAX_VALUE);
    }
}
