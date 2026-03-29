import axios from 'axios';

const axiosClient = axios.create({
    // Đảm bảo số 5248 này đúng với cổng Backend đang chạy của đại ca
    baseURL: 'http://localhost:5248/api', 
    headers: {
        'Content-Type': 'application/json',
    },
});

export default axiosClient;