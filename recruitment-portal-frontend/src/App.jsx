import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import LoginPage from './pages/LoginPage';
import JobListingPage from './pages/JobListingPage';
import ApplyJobPage from './pages/ApplyJobPage';
import Layout from './components/Layout';
import PostJobPage from './pages/PostJobPage';
const ProtectedRoute = ({ children }) => {
  const token = localStorage.getItem('authToken');
  return token ? children : <Navigate replace to="/login" />;
};

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/login" element={<LoginPage />} />
        <Route path="/apply/:jobId" element={<ApplyJobPage />} />
        <Route
          path="/jobs"
          element={
            <JobListingPage />
          }
        />

        <Route element={
          <ProtectedRoute>
            <Layout />
          </ProtectedRoute>
        }>
          <Route
            path="/"
            element={
              <ProtectedRoute>
                <div style={{ padding: '20px' }}>
                  <h1>Dashboard</h1>
                  <p>Welcome to the Recruitment Portal. You are logged in!</p>
                  <button onClick={() => {
                    localStorage.clear();
                    window.location.href = '/login';
                  }}>Logout</button>
                </div>
              </ProtectedRoute>
            }
          />
          <Route path="/jobs/create" element={<PostJobPage />} />
          <Route path="*" element={<Navigate to="/" />} />
        </Route>
      </Routes>
    </Router>
  );
}

export default App;