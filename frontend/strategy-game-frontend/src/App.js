import {
  BrowserRouter as Router,
  Routes,
  Route,
} from 'react-router-dom'
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

  return (
    <ThemeProvider theme={MainTheme}>
      <Router>
        <BodyWrapper>
          <Header />
          <Routes>
            <Route exact path="/" element={<Landing />} />
            <Route exact path="/login" element={<Login />} />
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
            pauseOnFocusLoss
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