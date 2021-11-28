import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';
import Typography from '@material-ui/core/Typography';
import { makeStyles, useTheme } from '@material-ui/core/styles';
import { useNavigate } from "react-router-dom"
import axios from 'axios'

import background from "assets/images/login-background.jpg"

import { useFormik } from 'formik';
import validationSchema from 'validation/Login/Schemas'
import styled from "styled-components"
import { useState } from 'react';

import { errorToast, successToast } from 'components/common/Toast/Toast'
import { useDispatch } from 'react-redux';
import { login } from 'redux/slices/HeaderSlice'

const LoginWrapper = styled.div`
    width: 100%;
    display: grid;
    place-items: center;
    padding: 3rem;
`
const FormWrapper = styled.div`
    background: #fff;
    border: 1px solid lightgrey;
    border-radius: 22px;
    width 100%;
    padding: 2rem;
    max-width: 1000px;
    display: flex;
    flex-direction: column;
    align-items: center;
`
const FieldsWrapper = styled.div`
    width: 100%;
    margin: 0.5rem 1rem;
`

const TitleWrapper = styled.div`
    width: 100%;
    margin: 1rem;
`

const useStyles = makeStyles((theme) => ({
    root: {
        height: '100vh',
        fontFamily: "raleway !important",
    },
    image: {
        background: `url(${background})`,
        backgroundRepeat: 'no-repeat',
        backgroundColor:
            theme.palette.type === 'light' ? theme.palette.grey[50] : theme.palette.grey[900],
        backgroundSize: 'cover',
        backgroundPosition: 'center',
    },
    paper: {
        margin: theme.spacing(8, 4),
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
    },
    avatar: {
        margin: theme.spacing(1),
        backgroundColor: theme.palette.primary.main,
    },
    form: {
        width: '100%', // Fix IE 11 issue.
        marginTop: theme.spacing(1),
    },
    submit: {
        fontFamily: "raleway",
        margin: theme.spacing(3, 0, 2),
        backgroundColor: theme.palette.primary.main,
        color: theme.palette.primary.contrastText,
        "&:hover, &:focus": {
            backgroundColor: theme.palette.primary.main,
            color: theme.palette.primary.contrastText,
        }
    },
    text: {
        fontFamily: "raleway"
    }
}));



const Login = () => {
    const theme = useTheme()
    const classes = useStyles(theme);

    const dispatch = useDispatch()

    const [response, setResponse] = useState(false)
    const [error, setError] = useState()
    const [loading, setLoading] = useState(false)
    const [status, setStatus] = useState()

    const handleLogin = async (values) => {
        try {
            setLoading(true)
            const res = await axios.post(
                'https://localhost:44365/api/user/login',
                values
            )
            setLoading(false)
            setStatus(res.status)
            if (res.status === 200) {
                setResponse(true)
                localStorage.setItem("token", res.data.token)
                successToast("Operation successful!")
            }
            return res
        } catch (err) {
            errorToast(err)
            setError(err)
            console.log(err)
        }
    }

    const navigate = useNavigate()

    const formik = useFormik({
        initialValues: {
            email: '',
            password: '',
        },
        validationSchema: validationSchema,
        onSubmit: async (values) => {
            await handleLogin(values)
                .then(res => {
                    if (res.status === 200 && localStorage.getItem("token")) {
                        dispatch(login({ token: localStorage.getItem("token") }))
                        navigate("/auth/game/status")
                    }
                })
        },
    });


    return (
        <LoginWrapper>
            <FormWrapper>
                <TitleWrapper>
                    <Typography component="h1" variant="h5" className={classes.text}>
                        Login
                    </Typography>
                </TitleWrapper>
                <FieldsWrapper>
                    <form className={classes.form} onSubmit={formik.handleSubmit}>
                        <TextField
                            variant="outlined"
                            margin="normal"
                            required
                            fullWidth
                            id="email"
                            label="Email address"
                            name="email"
                            autoComplete="email"
                            autoFocus
                            value={formik.values.email}
                            onChange={formik.handleChange}
                            error={formik.touched.email && Boolean(formik.errors.email)}
                            helperText={formik.touched.email && formik.errors.email}
                        />
                        <TextField
                            variant="outlined"
                            margin="normal"
                            required
                            fullWidth
                            name="password"
                            label="Password"
                            type="password"
                            id="password"
                            autoComplete="current-password"
                            value={formik.values.password}
                            onChange={formik.handleChange}
                            error={formik.touched.password && Boolean(formik.errors.password)}
                            helperText={formik.touched.password && formik.errors.password}
                        />
                        <Button
                            type="submit"
                            fullWidth
                            variant="contained"
                            className={classes.submit}
                        >
                            Login
                        </Button>
                    </form>
                </FieldsWrapper>

            </FormWrapper>


        </LoginWrapper>
    );
}

export default Login