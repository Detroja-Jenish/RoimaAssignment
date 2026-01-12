import React, { useState } from 'react';
import { authService } from '../services/authService';

const LoginPage = () => {
    const [credentials, setCredentials] = useState({ email: '', password: '' });

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const user = await authService.login(credentials.email, credentials.password);
            alert(`Welcome, ${user.firstName}!`);
            window.location.href = '/dashboard';
        } catch (err) {
            alert('Login failed: ' + err.message);
        }
    };

    return (
        <div style={{ padding: '2rem', maxWidth: '400px', margin: 'auto' }}>
            <h2>Recruitment Portal Login</h2>
            <form onSubmit={handleSubmit}>
                <input
                    type="email"
                    placeholder="Email"
                    value={credentials.email}
                    onChange={(e) => setCredentials({ ...credentials, email: e.target.value })}
                    style={{ display: 'block', width: '100%', marginBottom: '10px' }}
                />
                <input
                    type="password"
                    placeholder="Password"
                    value={credentials.password}
                    onChange={(e) => setCredentials({ ...credentials, password: e.target.value })}
                    style={{ display: 'block', width: '100%', marginBottom: '10px' }}
                />
                <button type="submit">Login</button>
            </form>
        </div>
    );
};

export default LoginPage;