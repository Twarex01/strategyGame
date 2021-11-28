import { createSlice, createAsyncThunk } from '@reduxjs/toolkit'
import axios from 'axios'

import { errorToast } from 'components/common/Toast/Toast'


const url = 'https://localhost:44365/api/command/gather/inprogress'

export const getAllGathering = createAsyncThunk('gatheringSlice/getAllGathering', async (props) => {
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
  response: null
}

const initialState = {
  gatherings: null,
  get: initialMethodState,
}

export const gatheringSlice = createSlice({
  name: 'gatheringSlice',
  initialState,
  reducers: {
  },
  extraReducers: {
    [getAllGathering.pending]: (state, action) => {
      state.get.status = 'loading'
    },
    [getAllGathering.fulfilled]: (state, action) => {
      state.get.status = 'succeeded'
      state.gatherings = action.payload.data
    },
    [getAllGathering.rejected]: (state, action) => {
      state.get.status = 'failed'
      state.get.error = action.error
      errorToast(state.get.error)
    }
  },
})

export default gatheringSlice.reducer