import axios from "axios";

const api = axios.create({
    headers: {
        'Content-Type': 'application/json'
    },
    baseURL: 'https://localhost:5001/api'
});

export default api;