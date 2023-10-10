import http from "k6/http";
import { check } from "k6";
import { API_BASE_URL, EMPLOYEES_ENDPOINT as ENDPOINT } from "../config.js";

export function setup() {
  const requestUrl = `${API_BASE_URL}/${__ENV.ORM}/${ENDPOINT}`;
  console.log(`requestUrl: ${requestUrl}`);

  return { requestUrl };
}

export default function ({ requestUrl }) {
  const params = {
    headers: { "Content-Type": "application/json" },
  };

  const payload = {
    firstName: "Jhon",
    lastName: "Doe",
    gender: 1,
    email: "jhon.doe@example.org",
    hireDate: "2022-01-03T05:00:00Z",
    salary: 8000,
    isActive: true,
    hourlyRate: 45,
    maritalStatus: 1,
    address: {
      street: "North Los Robles Avenue",
      city: "Pasadena",
      state: "CA",
      zipCode: "2311",
    },
    departmentId: "e8f9e70b-fb08-49ac-a1e7-d2e84cfbc7e8",
    projects: [
      "381d671b-508b-4326-a2fb-ce8b25197fbb",
      "c6c7cc44-b814-4299-a270-1d5acce9a662",
    ],
  };

  const result = http.post(requestUrl, JSON.stringify(payload), params);

  check(result, {
    "Status 201": (r) => {
        const body = JSON.parse(r.body);
        return body.status === 201},
  });
}
