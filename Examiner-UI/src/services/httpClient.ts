// src/services/httpClient.ts

import axios from 'axios';

const httpClient = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL, // use .env for environment config
  timeout: 10000,
  headers: {
    'Content-Type': 'application/json',
  },
});

// Optional: interceptors for auth, logging, etc.
// httpClient.interceptors.request.use(
//   (config) => {
//     const token = localStorage.getItem('auth_token');
//     if (token) {
//       config.headers.Authorization = `Bearer ${token}`;
//     }
//     return config;
//   },
//   (error) => Promise.reject(error)
// );

httpClient.interceptors.response.use(
  (response) => response,
  (error) => {
    // Global error handling logic
    return Promise.reject(error);
  }
);

export default httpClient;
