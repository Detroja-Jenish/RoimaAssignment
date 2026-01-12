import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { skillService } from '../services/skillService';
import { jobService } from '../services/jobService';
import styles from '../styles/postJob.module.css';

const PostJobPage = () => {
    const navigate = useNavigate();
    const [skills, setSkills] = useState([]);
    const [formData, setFormData] = useState({
        title: '',
        description: '',
        requiredMinExperience: '',
        lastDate: '', // Temporary string for input
        noOfOpenings: 0,
        importedSkillIDs: [],
        preferedSkillIDs: []
    });

    useEffect(() => {
        skillService.getAll().then(setSkills).catch(console.error);
    }, []);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData({ ...formData, [name]: value });
    };

    const handleMultiSelect = (e, fieldName) => {
        const values = Array.from(e.target.selectedOptions, option => parseInt(option.value));
        setFormData({ ...formData, [fieldName]: values });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        const dateInput = new Date(formData.lastDate);
        const formattedDate = dateInput.toISOString().split('T')[0];

        const payload = {
            title: formData.title,
            description: formData.description,
            requiredMinExperience: formData.requiredMinExperience.toString(),
            lastDateOFRegistration: formattedDate,
            noOfOpenings: parseInt(formData.noOfOpenings),
            createdBy: 1,
            importedSkillIDs: formData.importedSkillIDs,
            preferedSkillIDs: formData.preferedSkillIDs
        };

        try {

            await jobService.create(payload);
            alert('Job posted successfully!');
            navigate('/jobs');
        } catch (err) {
            console.error("Submission Error:", err);
            alert('Error: ' + err.message);
        }
    };

    return (
        <div className={styles.container}>
            <h2>Post a New Job Opening</h2>
            <form onSubmit={handleSubmit} className={styles.formGrid}>

                <div className={`${styles.formGroup} ${styles.fullWidth}`}>
                    <label className={styles.label}>Job Title</label>
                    <input className={styles.input} name="title" onChange={handleChange} required />
                </div>

                <div className={`${styles.formGroup} ${styles.fullWidth}`}>
                    <label className={styles.label}>Description</label>
                    <textarea className={styles.input} name="description" rows="4" onChange={handleChange} required />
                </div>

                <div className={styles.formGroup}>
                    <label className={styles.label}>Min Experience (Years)</label>
                    <input className={styles.input} type="number" name="requiredMinExperience" onChange={handleChange} required />
                </div>

                <div className={styles.formGroup}>
                    <label className={styles.label}>No. of Openings</label>
                    <input className={styles.input} type="number" name="noOfOpenings" onChange={handleChange} required />
                </div>

                <div className={styles.formGroup}>
                    <label className={styles.label}>Registration Deadline</label>
                    <input className={styles.input} type="date" name="lastDate" onChange={handleChange} required />
                </div>

                <div className={styles.formGroup}>
                    {/* Placeholder to maintain grid */}
                </div>

                <div className={styles.formGroup}>
                    <label className={styles.label}>Required Skills (Hold Ctrl/Cmd to select multiple)</label>
                    <select
                        multiple
                        className={`${styles.select} styles.multiSelect`}
                        onChange={(e) => handleMultiSelect(e, 'importedSkillIDs')}
                    >
                        {skills.map(s => <option key={s.skillID} value={s.skillID}>{s.skillTitle}</option>)}
                    </select>
                </div>

                <div className={styles.formGroup}>
                    <label className={styles.label}>Preferred Skills</label>
                    <select
                        multiple
                        className={`${styles.select} styles.multiSelect`}
                        onChange={(e) => handleMultiSelect(e, 'preferedSkillIDs')}
                    >
                        {skills.map(s => <option key={s.skillID} value={s.skillID}>{s.skillTitle}</option>)}
                    </select>
                </div>

                <button type="submit" className={`${styles.submitBtn} ${styles.fullWidth}`}>
                    Create Job Opening
                </button>
            </form>
        </div>
    );
};

export default PostJobPage;