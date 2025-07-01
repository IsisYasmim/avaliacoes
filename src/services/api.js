import axios from 'axios';

const isProduction = process.env.NODE_ENV === 'production';
const baseURL = isProduction 
  ? 'http://avaliacoes-service' 
  : 'http://localhost:5003';

const API = axios.create({
  baseURL,
  timeout: 10000,
});

// Add auth token to all requests
API.interceptors.request.use(config => {
  const token = localStorage.getItem('token');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

export default API;