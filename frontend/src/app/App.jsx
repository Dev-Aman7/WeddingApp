import { useSelector } from 'react-redux'
import { Navigate, Route, Routes } from 'react-router-dom'
import LoginPage from '../features/auth/LoginPage'
import SignupPage from '../features/auth/SignupPage'
import DashboardPage from '../features/dashboard/DashboardPage'
import { selectToken } from '../features/auth/authSlice'
import './App.css'

function App() {
  const token = useSelector(selectToken)

  return (
    <Routes>
      <Route path="/login" element={token ? <Navigate to="/dashboard" replace /> : <LoginPage />} />
      <Route path="/signup" element={token ? <Navigate to="/dashboard" replace /> : <SignupPage />} />
      <Route path="/dashboard" element={token ? <DashboardPage /> : <Navigate to="/login" replace />} />
      <Route path="*" element={<Navigate to={token ? '/dashboard' : '/login'} replace />} />
    </Routes>
  )
}

export default App
