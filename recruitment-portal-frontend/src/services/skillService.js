import { apiFetch } from './apiService';

export const skillService = {
    getAll: async () => {
        return await apiFetch('/Skill');
    }
};