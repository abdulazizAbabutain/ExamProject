import axios from "axios";


const api = axios.create({
    baseURL : 'https://localhost:7072',
    headers:{
        'Content-Type': 'application/json',
    }
});

export default api;
