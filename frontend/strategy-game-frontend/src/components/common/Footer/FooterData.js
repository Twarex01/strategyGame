import { FaGithub, FaUserAlt } from 'react-icons/fa';


const FooterData = {
    sections: [
        {
            name: "The team",
            links: [
                {
                    name: "Laki Balázs",
                    icon: <FaUserAlt />,
                    to: "https://www.youtube.com/watch?v=dQw4w9WgXcQ",
                },
                {
                    name: "Tóth Bence",
                    icon: <FaUserAlt />,
                    to: "https://www.youtube.com/watch?v=ZZ5LpwO-An4",
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