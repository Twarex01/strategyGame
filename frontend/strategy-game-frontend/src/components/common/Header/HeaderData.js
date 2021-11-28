import HomeIcon from '@material-ui/icons/Home'
import { BiLogIn } from "react-icons/bi"

const HeaderData = {
    icon: null,
    siteName: "SG",
    options: [
        {
            name: "Home",
            to: "/",
        }
    ],
    optionsAuth: [
        {
            name: "Status",
            to: "/auth/game/status",
        },
        {
            name: "Build",
            to: "/auth/game/build",
        },
        {
            name: "Fight",
            to: "/auth/game/fight",
        },
        {
            name: "Conquer",
            to: "/auth/game/conquer",
        },
        {
            name: "Toplist",
            to: "/auth/toplist",
        }
    ],
}

export default HeaderData;