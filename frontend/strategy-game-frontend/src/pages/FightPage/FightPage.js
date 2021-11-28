import styled from "styled-components"
import gameBackground from 'assets/images/game-background.jpg'
import Resource from "components/game/resource/Resource"
import { useEffect, useState, useRef } from "react"
import axios from "axios"
import { errorToast, infoToast, warningToast } from "components/common/Toast/Toast"
import textBackground from "assets/images/login-background.jpg"

import BuildMenu from "components/game/menus/BuildMenu"
import FightMenu from "components/game/menus/FightMenu"

import { HubConnectionBuilder } from '@microsoft/signalr'
import { useSelector } from "react-redux"

import { useNavigate } from "react-router"

const FightPageWrapper = styled.div`
    width: 100%;
    display: flex;
    flex-direction: column;
    align-items: center;

    @media(min-width: 600px){
        
    }`

const InnerMenuWrapper = styled.div`
    margin: 2rem 0;
    padding: 1rem 0;
    width: 100%;
    max-width: 750px;
    background-image: url(${gameBackground});

    @media(min-width: 600px){
        
    }
`

const FightPage = () => {

    const [resources, setResources] = useState([])
    const [resErr, setResErr] = useState()
    const [resLoading, setResLoading] = useState(false)

    console.log({ resources })
    const token = useSelector(store => store.persistedReducers.headerSliceReducer.token)

    const fetchData = async () => {
        try {
            setResLoading(true)
            const res = await axios.get(
                'https://localhost:44365/api/resource/all',
                {
                    headers: {
                        Authorization: `Bearer ${token}`
                    }
                }
            )
            console.log({ res })
            setResources(res.data)
            setResLoading(false)
        } catch (err) {
            errorToast(err)
            console.log(err)
            setResErr(err)
        }
    }

    useEffect(() => {
        fetchData()
        return () => {
            setResources([])
        }
    }, [])

    const [connection, setConnection] = useState(null);

    useEffect(() => {
        const newConnection = new HubConnectionBuilder()
            .withUrl('https://localhost:44365/roundhub')
            .withAutomaticReconnect()
            .build();
        setConnection(newConnection);
        return () => {
            setConnection(null)
        }
    }, []);

    useEffect(() => {
        console.log(connection)
        if (connection) {
            connection.start()
                .then(result => {
                    console.log('Connected!');
                })
                .catch(e => console.log('Connection failed: ', e));
            connection.on('TurnEnded', null);
            connection.on('TurnEnded', () => {
                console.log("Turn Ended")
                fetchData()
                infoToast("A turn has ended!")
            });
        }
        return () => {
            if (connection) {
                connection.on('TurnEnded', null)
            }
        }
    }, [connection]);



    const [armyRes, setArmyRes] = useState([])
    const [buildingRes, setBuildingRes] = useState([])



    useEffect(() => {
        const calcArmy = () => {
            let newArmyRes = []

            resources.forEach(res => {
                if (res.type === 4) newArmyRes.push(res)
            })
            setArmyRes(newArmyRes)

        }
        const calcBuildingResources = () => {
            let newBuildingRes = []

            resources.forEach(res => {
                if (!(res.type === 4)) newBuildingRes.push(res)
            })
            setBuildingRes(newBuildingRes)
        }
        calcArmy()
        calcBuildingResources()
    }, [resources])

    const [modalOpen, setModalOpen] = useState(false)
    const [modalContent, setModalContent] = useState()

    const openBuildMenu = () => {
        setModalContent(<BuildMenu fetchData={fetchData} setModalOpen={setModalOpen} />)
        setModalOpen(true)
    }
    const openFightMenu = () => {
        setModalContent(<FightMenu fetchData={fetchData} setModalOpen={setModalOpen} />)
        setModalOpen(true)
    }

    const navigate = useNavigate()
    const handleNavigate = (path) => {
        navigate(path)
    }

    return (
        <FightPageWrapper>
            <InnerMenuWrapper>
                <FightMenu fetchData={fetchData} setModalOpen={setModalOpen} />
            </InnerMenuWrapper>
        </FightPageWrapper>
    );
}

export default FightPage