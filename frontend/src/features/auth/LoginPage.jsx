import { useState } from 'react'
import { useNavigate } from 'react-router-dom'
import { useDispatch } from 'react-redux'
import { useLoginMutation } from '../api/apiSlice'
import { setToken } from './authSlice'
import '../../app/App.css'

export default function LoginPage() {
  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')
  const [login, { isLoading }] = useLoginMutation()
  const [error, setError] = useState('')
  const navigate = useNavigate()
  const dispatch = useDispatch()

  const handleSubmit = async (e) => {
    e.preventDefault()
    setError('')
    try {
      const result = await login({ email, password }).unwrap()
      dispatch(setToken(result.token))
      navigate('/dashboard')
    } catch {
      setError('Invalid email or password.')
    }
  }

  return (
    <div className="login-page">
      <div className="login-illustration" aria-hidden="true">
        <div className="login-illustration__inner">
          <span className="login-illustration__icon">💍</span>
          <h2>Wedding Planner</h2>
          <p>Seamlessly manage guests, RSVPs, and your special day.</p>
        </div>
      </div>

      <div className="login-form-wrapper">
        <form className="login-card" onSubmit={handleSubmit}>
          <div className="login-card__header">
            <h1>Welcome back</h1>
            <p>Please enter your details to sign in.</p>
          </div>

          <label className="field">
            <span className="field__label">Email</span>
            <input
              className="field__input"
              type="email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              placeholder="you@example.com"
              required
            />
          </label>

          <label className="field">
            <span className="field__label">Password</span>
            <input
              className="field__input"
              type="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              placeholder="••••••••"
              required
            />
          </label>

          {error ? <p className="login-card__error" role="alert">{error}</p> : null}

          <button className="login-card__submit" type="submit" disabled={isLoading}>
            {isLoading ? 'Signing in...' : 'Sign in'}
          </button>

          <p className="auth-link-row">
            New here? <a className="auth-link" href="/signup">Create an account</a>
          </p>
        </form>
        <p className="login-hint">Use <strong>admin@example.com</strong> / <strong>password123</strong></p>
      </div>
    </div>
  )
}
