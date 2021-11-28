import { FaGithub, FaUserAlt } from 'react-icons/fa';


const FooterData = {
    sections: [
        {
            name: "The team",
            links: [
                {
                    name: "Laki Balázs",
                    icon: <FaUserAlt />,
                    to: "#",
                },
                {
                    name: "Tóth Bence",
                    icon: <FaUserAlt />,
                    to: "#",
                },
            ],
        },
        {
            name: "Github",
            links: [
                {
                    name: "Laki Balázs",
                    icon: <FaGithub />,
                    to: "https://github.com/Twarex01",
                },
                {
                    name: "Tóth Bence",
                    icon: <FaGithub />,
                    to: "https://github.com/tothbence9922",
                }
            ],
        }
    ]
}

export default FooterData;