import { createSlice } from '@reduxjs/toolkit'

export const headerSlice = createSlice({
  name: 'header',
  initialState: {
    active: null,
  },
  reducers: {
    setActive: (state, action) => {
      state.active = action.payload
    },
  },
})

// Action creators are generated for each case reducer function
export const { setActive } = headerSlice.actions

export default headerSlice.reducer