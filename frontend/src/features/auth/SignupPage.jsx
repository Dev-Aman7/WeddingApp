import { useState } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import { useLoginMutation, useSignupMutation } from '../api/apiSlice'
import { setToken } from './authSlice'
import '../../app/App.css'

export default function SignupPage() {
  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')
  const [confirmPassword, setConfirmPassword] = useState('')
  const [signup, { isLoading: isSigningUp }] = useSignupMutation()
  const [login, { isLoading: isLoggingIn }] = useLoginMutation()
  const [error, setError] = useState('')
  const [success, setSuccess] = useState('')
  const navigate = useNavigate()

  const handleSubmit = async (e) => {
    e.preventDefault()
    setError('')
    setSuccess('')

    if (password !== confirmPassword) {
      setError('Passwords do not match.')
      return
    }

    try {
      await signup({ email, password }).unwrap()
      const result = await login({ email, password }).unwrap()
      setToken(result.token)
      setSuccess('Account created successfully. Welcome to your wedding dashboard!')
      navigate('/dashboard')
    } catch {
      setError('We could not create your account. Please try again.')
    }
  }

  return (
    <div className="login-page">
      <div className="login-illustration" aria-hidden="true">
        <div className="login-illustration__inner">
          <span className="login-illustration__icon">💐</span>
          <h2>Start your celebration</h2>
          <p>Create an account to coordinate guests, follow your plans, and make every detail feel effortless.</p>
        </div>
      </div>

      <div className="login-form-wrapper">
        <form className="login-card" onSubmit={handleSubmit}>
          <div className="login-card__header">
            <h1>Create your account</h1>
            <p>Join the wedding dashboard and keep everything beautifully organized.</p>
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
              placeholder="Create a secure password"
              required
            />
          </label>

          <label className="field">
            <span className="field__label">Confirm password</span>
            <input
              className="field__input"
              type="password"
              value={confirmPassword}
              onChange={(e) => setConfirmPassword(e.target.value)}
              placeholder="Confirm your password"
              required
            />
          </label>

          {error ? <p className="login-card__error" role="alert">{error}</p> : null}
          {success ? <p className="login-card__success">{success}</p> : null}

          <button className="login-card__submit" type="submit" disabled={isSigningUp || isLoggingIn}>
            {isSigningUp || isLoggingIn ? 'Creating your account...' : 'Create account'}
          </button>

          <p className="auth-link-row">
            Already have an account? <Link to="/login" className="auth-link">Sign in</Link>
          </p>
        </form>
      </div>
    </div>
  )
}
