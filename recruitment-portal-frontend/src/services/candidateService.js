import { apiFetch } from './apiService';

export const candidateService = {
    register: async (formData) => {
        return await apiFetch('/Candidates', {
            method: 'POST',
            body: formData,
        });
    }
};