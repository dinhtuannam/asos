import axios from 'axios';
import { getToken } from '@/helpers/storage.helper';

function getAxios(baseURL: string) {
    const ins = axios.create({
        baseURL,
    });
    ins.interceptors.request.use(function (config) {
        const accessToken = getToken();

        if (accessToken) {
            config.headers['Authorization'] = `Bearer ${accessToken}`;
        }
        config.headers['Content-Type'] = 'application/json';

        return config;
    });

    return ins;
}

export const API = getAxios(`https://localhost:7000`);
