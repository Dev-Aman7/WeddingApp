import { createSlice } from '@reduxjs/toolkit'

const authSlice = createSlice({
  name: 'auth',
  initialState: {
    token: localStorage.getItem('token'),
  },
  reducers: {
    setToken(state, action) {
      state.token = action.payload
      if (action.payload) {
        localStorage.setItem('token', action.payload)
      } else {
        localStorage.removeItem('token')
      }
    },
  },
})

export const { setToken } = authSlice.actions
export const selectToken = (state) => state.auth.token
export default authSlice.reducer
