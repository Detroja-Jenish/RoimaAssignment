import { apiFetch } from './apiService';

export const authService = {
    login: async (email, password) => {
        const data = await apiFetch('/Auth', {
            method: 'POST',
            body: JSON.stringify({ email, password }),
        });


        if (data.result?.authToken) {
            localStorage.setItem('authToken', data.result.authToken);
            localStorage.setItem('user', JSON.stringify(data.result));
        }

        return data.result;
    },

    logout: () => {
        localStorage.removeItem('authToken');
        localStorage.removeItem('user');
    }
};