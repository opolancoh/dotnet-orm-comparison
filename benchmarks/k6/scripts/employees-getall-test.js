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

  const result = http.get(requestUrl, params);

  check(result, {
    "Status 200": (r) => {
        const body = JSON.parse(r.body);
        return body.status === 200},
  });
}
