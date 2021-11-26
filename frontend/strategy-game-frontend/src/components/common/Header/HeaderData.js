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
        }
    ],
    optionsAuth: [
        {
            name: "Játék",
            to: "/auth/game",
            icon: <HomeIcon />,
            onClick: (dispatch, setActive, idx) => {
                dispatch(setActive(idx));
            },
            divider: false,
            disabled: false,
        },
        {
            name: "Ranglista",
            to: "/auth/toplist",
            icon: <HomeIcon />,
            onClick: (dispatch, setActive, idx) => {
                dispatch(setActive(idx));
            },
            divider: false,
            disabled: false,
        }
    ],
}

export default HeaderData;