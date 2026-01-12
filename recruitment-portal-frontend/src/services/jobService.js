import { apiFetch } from './apiService';

export const jobService = {
    getAll: async () => {
        return await apiFetch('/JobOpening');
    },

    create: async (jobData) => {
        return await apiFetch('/JobOpening', {
            method: 'POST',
            body: JSON.stringify(jobData),
        });
    }
};