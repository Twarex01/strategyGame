import { createSlice, createAsyncThunk } from '@reduxjs/toolkit'
import axios from 'axios'

import { errorToast } from 'components/common/Toast/Toast'


const url = 'https://localhost:44365/api/command/attack/inprogress'

export const getAllFights = createAsyncThunk('fightSlice/getAllFights', async (props) => {
  const path = url
  const token = localStorage.getItem("token")
  const response = await axios.get(
    path,
    {
      headers: {
        Authorization: `Bearer ${token}`
      }
    })
  return response
})

const initialMethodState = {
  status: 'idle',
  error: null,
  response: []
}

const initialState = {
  fights: [],
  get: initialMethodState,
}

export const fightSlice = createSlice({
  name: 'fightSlice',
  initialState,
  reducers: {
  },
  extraReducers: {
    [getAllFights.pending]: (state, action) => {
      state.get.status = 'loading'
    },
    [getAllFights.fulfilled]: (state, action) => {
      state.get.status = 'succeeded'
      state.fights = [...action.payload.data]
    },
    [getAllFights.rejected]: (state, action) => {
      state.get.status = 'failed'
      state.get.error = action.error
      errorToast(state.get.error)
    }
  },
})

export default fightSlice.reducer