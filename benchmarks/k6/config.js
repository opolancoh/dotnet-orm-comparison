const API_BASE_URL = 'https://localhost:7215/api';
const EMPLOYEES_ENDPOINT = 'employees';

export { API_BASE_URL, EMPLOYEES_ENDPOINT };

export const smokeTestOptions = {
  vus: 1,
  duration: '30s',
  thresholds: {
    http_req_duration: ['p(95)<100'],
  },
};