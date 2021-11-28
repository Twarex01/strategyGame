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
import { useSelector } from 'react-redux'

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

  const auth = useSelector(store => store.persistedReducers.headerSliceReducer.auth)

  return (
    <ThemeProvider theme={MainTheme}>
      <Router>
        <BodyWrapper>
          <Header />
          <Routes>
            <Route exact path="/" element={<Landing />} />
            <Route exact path="/login" element={<Login />} />

            <Route exact path="/auth/game/status" element={auth ? <Game /> : <Navigate to="/login" />} />
            <Route exact path="/auth/game/build" element={auth ? <BuildPage /> : <Navigate to="/login" />} />
            <Route exact path="/auth/game/fight" element={auth ? <FightPage /> : <Navigate to="/login" />} />
            <Route exact path="/auth/toplist" element={auth ? <Toplist /> : <Navigate to="/login" />} />
            <Route path="*" element={<NotFound />} />
          </Routes>

          <ToastContainer
            position="bottom-right"
            autoClose={5000}
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