import HomeIcon from '@material-ui/icons/Home'
import { BiLogIn } from "react-icons/bi"

const HeaderData = {
    icon: null,
    siteName: "SG",
    options: [
        {
            name: "Home",
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
            name: "Game",
            to: "/auth/game",
            icon: <HomeIcon />,
            onClick: (dispatch, setActive, idx) => {
                dispatch(setActive(idx));
            },
            divider: false,
            disabled: false,
        },
        {
            name: "Toplist",
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