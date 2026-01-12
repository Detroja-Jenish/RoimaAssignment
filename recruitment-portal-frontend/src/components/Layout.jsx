import React from 'react';
import { Outlet, useNavigate, Link } from 'react-router-dom';
import styles from '../styles/layout.module.css'
const Layout = () => {
    const navigate = useNavigate();
    const user = JSON.parse(localStorage.getItem('user'));

    const handleLogout = () => {
        localStorage.clear();
        navigate('/login');
    };

    return (
        <div className={styles.container}>
            {/* Navigation Bar */}
            <nav className={styles.navbar}>
                <div className={styles.navContent}>
                    <Link to="/" className={styles.logo}>RecruitPortal</Link>

                    <div className={styles.navLinks}>
                        <Link to="/jobs" className={styles.link}>Available Jobs</Link>
                        {user ? (
                            <div className={styles.userSection}>
                                <span className={styles.userName}>Hello, {user.firstName}</span>
                                <button onClick={handleLogout} className={styles.logoutBtn}>Logout</button>
                            </div>
                        ) : (
                            <Link to="/login" className={styles.loginBtn}>HR Login</Link>
                        )}
                    </div>
                </div>
            </nav>

            {/* Main Content Area */}
            <main className={styles.main}>
                <div className={styles.pageCard}>
                    <Outlet />
                </div>
            </main>

            {/* Simple Footer */}
            <footer className={styles.footer}>
                &copy; {new Date().getFullYear()} Recruitment Management System
            </footer>
        </div>
    );
};

export default Layout;