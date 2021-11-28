import {
  BrowserRouter as Router,
  Routes,
  Route,
} from 'react-router-dom'
import { Navigate } from "react-router-dom"
import styled from 'styled-components'

import { ThemeProvider } from '@material-ui/styles'
import MainTheme from 'styles/MainTheme'

import Header from 'components/common/Header/Header'
import Footer from 'components/common/Footer/Footer'

import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import Landing from './pages/Landing/Landing'
import Login from './pages/Login/Login'
import NotFound from './pages/NotFound/NotFound'

import Toplist from './pages/Toplist/Toplist'
import Game from './pages/Game/Game'
import BuildPage from 'pages/BuildPage/BuildPage'
import FightPage from 'pages/FightPage/FightPage'
import ConquerPage from 'pages/ConquerPage/ConquerPage'
import { useDispatch, useSelector } from 'react-redux'
import { useEffect } from 'react'
import Connection from 'signalr/Signalr'
import { infoToast } from 'components/common/Toast/Toast'
import { getAll as getAllResources } from 'redux/slices/ResourceSlice'
import { getAllFights } from 'redux/slices/FightSlice'
import { getAllGathering } from 'redux/slices/GatheringSlice'

const BodyWrapper = styled.div`
    min-height: 100vh;
    display: flex;
    flex-direction: column;
    background: #dedede;
`

const FooterWrapper = styled.div`
  margin-top: auto;
`

function App() {

  const dispatch = useDispatch()

  useEffect(() => {
    Connection.on('TurnEnded', turnEnded => {
      infoToast("A turn has ended!")
      dispatch(getAllResources())
    });
    Connection.on('AttackEnded', attackEnded => {
      infoToast(`${attackEnded.unitsLost} units lost...`)
      infoToast(`Attack ${attackEnded.success ? "successful" : "failed"}!`)
    });
    Connection.on('DefenseEnded', defenseEnded => {
      infoToast(`${defenseEnded.unitsLost} units lost...`)
      infoToast(`Defense ${defenseEnded.success ? "successful" : "failed"}!`)
    });
    Connection.on('GatherDone', gatherDone => {
      infoToast("Gathering done!")
    });
    Connection.on('Tick', tickmsg => {
      dispatch(getAllGathering())
      dispatch(getAllFights())
    });
  }, [])

  const auth = useSelector(store => store.persistedReducers.headerSliceReducer.auth)

  return (
    <ThemeProvider theme={MainTheme}>
      <Router>
        <BodyWrapper>
          <Header />
          <Routes>
            <Route exact path="/" element={auth ? <Navigate to="/auth/game/status" /> : <Landing />} />
            <Route exact path="/login" element={auth ? <Navigate to="/auth/game/status" /> : <Login />} />

            <Route exact path="/auth/game/status" element={auth ? <Game /> : <Navigate to="/login" />} />
            <Route exact path="/auth/game/build" element={auth ? <BuildPage /> : <Navigate to="/login" />} />
            <Route exact path="/auth/game/fight" element={auth ? <FightPage /> : <Navigate to="/login" />} />
            <Route exact path="/auth/game/conquer" element={auth ? <ConquerPage /> : <Navigate to="/login" />} />
            <Route exact path="/auth/toplist" element={auth ? <Toplist /> : <Navigate to="/login" />} />
            <Route path="*" element={<NotFound />} />
          </Routes>

          <ToastContainer
            position="bottom-right"
            autoClose={2500}
            hideProgressBar
            newestOnTop
            limit={5}
            closeOnClick
            rtl={false}
            draggable
            pauseOnHover
          />
          <FooterWrapper>
            <Footer />
          </FooterWrapper>
        </BodyWrapper>
      </Router>
    </ThemeProvider>
  );
}

export default App;