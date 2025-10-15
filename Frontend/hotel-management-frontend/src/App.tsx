import { Routes, Route, Navigate } from 'react-router-dom';
import Login from './pages/Auth/Login.tsx';
import { useEffect } from 'react';
import { getUserRole } from './utils/auth.ts';
import AdminLogin from './pages/Auth/AdminLogin.tsx';
import AdminDashboard from './pages/admin/AdminDashboard.tsx';
import AllBranchesPage from './pages/admin/AllBranchesPage.tsx';
function App() {
  useEffect(() => {
    const user = getUserRole();
    if (user) {
    } else {
    }
  }, []);
  return (
    <Routes>
      <Route path="/" element={<Navigate to="/login" replace />} />
      <Route path="/login" element={<Login />} />
      <Route path="/admin-login" element={<AdminLogin />} />
      <Route
        path="/admin/dashboard"
        element={
          <AdminDashboard>
            <h1>Welcome to Admin Dashboard</h1>
          </AdminDashboard>
        }
      />
      <Route path="/admin/all-branches" element={<AllBranchesPage />} />
    </Routes>
  );
}

export default App;


