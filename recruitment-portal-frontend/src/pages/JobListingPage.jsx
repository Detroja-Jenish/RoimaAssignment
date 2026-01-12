import React, { useEffect, useState } from 'react';
import { jobService } from '../services/jobService';
import { useNavigate } from 'react-router-dom';

const JobListingPage = () => {
    const [jobs, setJobs] = useState([]);
    const [loading, setLoading] = useState(true);
    const navigate = useNavigate();

    useEffect(() => {
        const fetchJobs = async () => {
            try {
                const data = await jobService.getAll();
                setJobs(data);
            } catch (err) {
                console.error('Failed to load jobs:', err);
            } finally {
                setLoading(false);
            }
        };
        fetchJobs();
    }, []);

    if (loading) return <div style={{ padding: '20px' }}>Loading job openings...</div>;

    return (
        <div style={{ padding: '30px', maxWidth: '1200px', margin: '0 auto', fontFamily: 'sans-serif' }}>
            <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '30px' }}>
                <h1 style={{ color: '#2c3e50' }}>Career Opportunities</h1>
                <button
                    onClick={() => navigate('/jobs/create')}
                    style={{ padding: '10px 20px', backgroundColor: '#27ae60', color: 'white', border: 'none', borderRadius: '5px', cursor: 'pointer' }}
                >
                    Post New Job
                </button>
            </div>

            <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fill, minmax(350px, 1fr))', gap: '25px' }}>
                {jobs.length === 0 ? (
                    <p>No job openings available at the moment.</p>
                ) : (
                    jobs.map((job) => (
                        <div key={job.jobOpeningId} style={{ border: '1px solid #e0e0e0', borderRadius: '10px', padding: '20px', boxShadow: '0 4px 6px rgba(0,0,0,0.05)', backgroundColor: '#fff' }}>
                            <h2 style={{ margin: '0 0 10px 0', color: '#2980b9' }}>{job.title}</h2>
                            <p style={{ color: '#7f8c8d', fontSize: '0.95rem', height: '60px', overflow: 'hidden' }}>{job.description}</p>

                            <div style={{ marginTop: '15px' }}>
                                <h4 style={{ marginBottom: '10px', fontSize: '0.9rem' }}>Required Skills:</h4>
                                <div style={{ display: 'flex', flexWrap: 'wrap', gap: '8px' }}>
                                    {job.skills?.map((skill) => (
                                        <span key={skill.skillID} style={{
                                            fontSize: '0.75rem',
                                            padding: '4px 10px',
                                            borderRadius: '15px',
                                            backgroundColor: skill.importance === 'High' ? '#ebedef' : '#f4f7f6',
                                            border: `1px solid ${skill.importance === 'High' ? '#c0392b' : '#bdc3c7'}`,
                                            color: skill.importance === 'High' ? '#c0392b' : '#2c3e50'
                                        }}>
                                            {skill.skillTitle}
                                        </span>
                                    ))}
                                </div>
                            </div>
                            <div style={{ marginTop: '20px', display: 'flex', justifyContent: 'flex-end' }}>
                                <button
                                    onClick={() => navigate(`/apply/${job.jobOpeningId}`)}
                                    style={{ padding: '10px 25px', borderRadius: '4px', border: 'none', color: 'white', background: '#27ae60', cursor: 'pointer', fontWeight: 'bold' }}
                                >
                                    Apply Now
                                </button>
                            </div>

                            <div style={{ marginTop: '20px', borderTop: '1px solid #eee', paddingTop: '15px', display: 'flex', justifyContent: 'space-between' }}>
                                <button
                                    onClick={() => navigate(`/jobs/${job.jobOpeningId}`)}
                                    style={{ padding: '5px 15px', borderRadius: '4px', border: '1px solid #3498db', color: '#3498db', background: 'white', cursor: 'pointer' }}
                                >
                                    Details
                                </button>
                            </div>
                        </div>
                    ))
                )}
            </div>
        </div>
    );
};

export default JobListingPage;