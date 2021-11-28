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
            onClick: (dispatch, setActive, idx) => {
                dispatch(setActive(idx));
            },
            divider: false,
            disabled: false,
        },
        {
            name: "Build",
            to: "/auth/game/build",
            onClick: (dispatch, setActive, idx) => {
                dispatch(setActive(idx));
            },
            divider: false,
            disabled: false,
        },
        {
            name: "Fight",
            to: "/auth/game/fight",
            onClick: (dispatch, setActive, idx) => {
                dispatch(setActive(idx));
            },
            divider: false,
            disabled: false,
        },
        {
            name: "Toplist",
            to: "/auth/toplist",
            onClick: (dispatch, setActive, idx) => {
                dispatch(setActive(idx));
            },
            divider: false,
            disabled: false,
        }
    ],
}

export default HeaderData;