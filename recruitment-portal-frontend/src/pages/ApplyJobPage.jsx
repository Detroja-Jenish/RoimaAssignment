import React, { useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { candidateService } from '../services/candidateService';

const ApplyJobPage = () => {
    const { jobId } = useParams(); // Retrieves AppliedJobOpeningId from URL
    const navigate = useNavigate();
    const [isSubmitting, setIsSubmitting] = useState(false);

    const [formData, setFormData] = useState({
        firstName: '',
        lastName: '',
        email: '',
        phone: '',
        experience: '',
        currentCompany: '',
        currentCTC: '',
        expectedCTC: '',
        cv: null // This will hold the binary file
    });

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setFormData({ ...formData, [name]: value });
    };

    const handleFileChange = (e) => {
        setFormData({ ...formData, cv: e.target.files[0] });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setIsSubmitting(true);

        // Prepare Multipart Form Data
        const data = new FormData();
        data.append('FirstName', formData.firstName);
        data.append('LastName', formData.lastName);
        data.append('Email', formData.email);
        data.append('Phone', formData.phone);
        data.append('Experience', formData.experience);
        data.append('CurrentCompany', formData.currentCompany);
        data.append('CurrentCTC', formData.currentCTC);
        data.append('ExpectedCTC', formData.expectedCTC);
        data.append('AppliedJobOpeningId', jobId);

        // The key 'CV' must match the parameter name in your C# Model/Controller
        if (formData.cv) {
            data.append('CV', formData.cv);
        }

        try {
            await candidateService.register(data);
            alert("Application sent successfully!");
            navigate('/jobs');
        } catch (error) {
            alert("Submission failed: " + error.message);
        } finally {
            setIsSubmitting(false);
        }
    };

    return (
        <div style={{ maxWidth: '500px', margin: '2rem auto', padding: '1rem', border: '1px solid #ccc', borderRadius: '8px' }}>
            <h2>Apply for Position</h2>
            <form onSubmit={handleSubmit}>
                <input style={inputStyle} name="firstName" placeholder="First Name" onChange={handleInputChange} required />
                <input style={inputStyle} name="lastName" placeholder="Last Name" onChange={handleInputChange} required />
                <input style={inputStyle} name="email" type="email" placeholder="Email" onChange={handleInputChange} required />
                <input style={inputStyle} name="phone" placeholder="Phone Number" onChange={handleInputChange} required />
                <input style={inputStyle} name="experience" type="number" step="0.1" placeholder="Years of Experience" onChange={handleInputChange} required />
                <input style={inputStyle} name="currentCompany" placeholder="Current Company" onChange={handleInputChange} />
                <input style={inputStyle} name="currentCTC" type="number" placeholder="Current CTC" onChange={handleInputChange} />
                <input style={inputStyle} name="expectedCTC" type="number" placeholder="Expected CTC" onChange={handleInputChange} />

                <div style={{ margin: '1rem 0' }}>
                    <label style={{ display: 'block', marginBottom: '5px' }}>Upload CV (PDF/Doc):</label>
                    <input type="file" accept=".pdf,.doc,.docx" onChange={handleFileChange} required />
                </div>

                <button
                    type="submit"
                    disabled={isSubmitting}
                    style={{ width: '100%', padding: '10px', backgroundColor: '#28a745', color: '#fff', border: 'none', borderRadius: '4px', cursor: 'pointer' }}
                >
                    {isSubmitting ? 'Uploading...' : 'Submit Application'}
                </button>
            </form>
        </div>
    );
};

const inputStyle = {
    display: 'block',
    width: '100%',
    padding: '8px',
    marginBottom: '10px',
    boxSizing: 'border-box'
};

export default ApplyJobPage;