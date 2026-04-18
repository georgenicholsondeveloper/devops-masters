import http from 'k6/http';
import { check } from 'k6';

export const options = {
  vus: 10,
  duration: '30s',
  thresholds: {
    http_req_duration: ['p(95)<500'],
    http_req_failed: ['rate<0.01'],
  },
};

export default function () {
  const baseUrl = __ENV.BaseUrl || 'http://localhost:8080';
  const hostName = __ENV.ApiHostName || 'localhost';
  
  const params = {
    headers: {
      'Host': hostName,
      'Accept': 'application/json',
    },
  };
  
  const res = http.get(`${baseUrl}/`, params);
  check(res, { 'status is 200': r => r.status === 200 });
}