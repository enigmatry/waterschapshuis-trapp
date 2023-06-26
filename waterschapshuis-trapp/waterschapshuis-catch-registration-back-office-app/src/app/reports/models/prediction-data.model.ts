import { PredictionRequest } from './prediction-request.model';
import { GetPredictionResponse } from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';

export class PredictionData {
    constructor(
        public request: PredictionRequest,
        public response: GetPredictionResponse) {
    }
}
