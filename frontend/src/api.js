import axios from 'axios';

const api = axios.create({
  baseURL: 'http://localhost:5117',
});

export default api;
