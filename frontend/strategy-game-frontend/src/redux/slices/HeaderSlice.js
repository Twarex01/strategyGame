import { createSlice } from '@reduxjs/toolkit'

export const headerSlice = createSlice({
  name: 'header',
  initialState: {
    auth: false,
  },
  reducers: {
    login: (state, action) => {
      state.auth = true
    },
    logout: (state, action) => {
      state.auth = false
    },
  },
})

// Action creators are generated for each case reducer function
export const { login, logout } = headerSlice.actions

export default headerSlice.reducer