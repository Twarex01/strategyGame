import HomeIcon from '@material-ui/icons/Home'
import { BiLogIn } from "react-icons/bi"

const HeaderData = {
    icon: null,
    siteName: "SG",
    options: [
        {
            name: "Főoldal",
            to: "/",
            icon: <HomeIcon />,
            onClick: (dispatch, setActive, idx) => {
                dispatch(setActive(idx));
            },
            divider: false,
            disabled: false,
        },
        {
            name: "Bejelentkezés",
            to: "/login",
            icon: <BiLogIn />,
            onClick: (dispatch, setActive, idx) => {
                dispatch(setActive(idx));
            },
            divider: false,
            disabled: false,
        },

    ],
}

export default HeaderData;