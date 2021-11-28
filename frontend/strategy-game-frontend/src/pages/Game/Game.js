import styled from "styled-components"
import gameBackground from 'assets/images/game-background.jpg'
import Resource from "components/game/resource/Resource"
import { useEffect, useState, useRef } from "react"
import axios from "axios"
import { errorToast } from "components/common/Toast/Toast"
import textBackground from "assets/images/login-background.jpg"

import BuildMenu from "components/game/menus/BuildMenu"
import FightMenu from "components/game/menus/FightMenu"

import { HubConnectionBuilder } from '@microsoft/signalr';

const GameWrapper = styled.div`
    width: 100%;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: space-around;
`

const SceneWrapper = styled.div`
    margin: 2rem;
    width: 100%;
    max-width: 1000px;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: flex-end;
    background-image: url(${gameBackground});
    height: 80vh;
    position: relative;
    box-shadow: 11px 11px 11px rgba(0, 0, 0, 0.3);

`

const ModalOuterWrapper = styled.div`
    width: 100vw;
    height: 100vh;
    z-index: 99;
    position: absolute;
    left: 0;    
    top: 0;    
`

const ModalWrapper = styled.div`
    width: 100%;
    max-width: 750px;
    z-index: 100;
    position: absolute;
    left: 50%;    
    top: 50%;    
    transform: translate(-50%, -50%);
    background-image: url(${textBackground});
    border-radius: 22px;
    box-shadow: 22px 22px 22px rgba(0, 0, 0, 0.6);
    padding: 1rem;
`

const ButtonsWrapper = styled.div`
    width: 100%;
    display: flex;
    flex-direction: row;
    justify-content: space-around;
    align-items: flex-end;
    background: #262729;
`

const BigButton = styled.div`
    display: grid;
    place-items: center;
    width: 100px;
    height: 100px;
    border-radius: 50%;
    border: 1px solid black;
    margin: 1rem 0;
    transition: 0.2s linear;

    box-shadow: 5px 5px 5px rgba(0, 0, 0, 0.3),
                inset 5px 5px 5px rgba(0, 0, 0, 0.3);

    background: lightgrey;
    font-size: 1.75rem;
    font-weight: 700;
    color: black;

    &:hover {
        cursor: pointer;
        background: grey;
        color: #FEFEFE;
        transition: 0.2s linear;
    }
`


const ResourceTypeWrapper = styled.div`
    display: flex;
    flex-direction: column;
    align-items: center;
    border: 1px solid white;
`

const ResourceType = styled.div`
    width: 100%;
    height: 25px;
    background-image: url(${textBackground});
    color: white;
    font-weight: 600;
    font-size: 1.25rem;
    display: grid;
    place-items: center;
`

const ResourceWrapper = styled.div`
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: flex-end;
    height: 25px;
`

const Game = () => {

    const [connection, setConnection] = useState(null);
    const [chat, setChat] = useState([]);
    const latestChat = useRef(null);

    latestChat.current = chat;

    useEffect( () => {
        alert(chat)
        console.log(chat)
    }, [chat])

    useEffect(() => {
        const newConnection = new HubConnectionBuilder()
            .withUrl('https://localhost:44365/roundhub')
            .withAutomaticReconnect()
            .build();
        setConnection(newConnection);
    }, []);

    useEffect(() => {
        alert("jó genyó")
        console.log(connection)
        if (connection) {
            connection.start()
                .then(result => {
                    console.log('Connected!');

                    connection.on('ReceiveMessage', message => {
                        const updatedChat = [...latestChat.current];
                        updatedChat.push(message);

                        setChat(updatedChat);
                        console.log(message)
                    });
                })
                .catch(e => console.log('Connection failed: ', e));
        }
    }, [connection]);
    const [resources, setResources] = useState([])

    const [resErr, setResErr] = useState()
    const [resLoading, setResLoading] = useState(false)

    const fetchData = async () => {
        try {
            setResLoading(true)
            let token = localStorage.getItem("token")
            const res = await axios.get(
                'https://localhost:44365/api/resource/all',
                {
                    headers: {
                        Authorization: `Bearer ${token}`
                    }
                }
            )
            setResLoading(false)
            setResources(res.data)
        } catch (err) {
            errorToast(err)
            console.log(err)
            setResErr(err)
        }
    }

    useEffect(() => {
        fetchData()
    }, [])

    const [armyRes, setArmyRes] = useState([])
    const [buildingRes, setBuildingRes] = useState([])

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

    useEffect(() => {
        calcArmy()
        calcBuildingResources()
    }, [resources])

    const [modalOpen, setModalOpen] = useState(false)
    const [modalContent, setModalContent] = useState()

    const toggleModalOpen = () => {
        setModalOpen(!modalOpen)
    }

    const openBuildMenu = () => {
        toggleModalOpen()
        setModalContent(<BuildMenu setModalOpen={setModalOpen} />)
    }
    const openFightMenu = () => {
        toggleModalOpen()
        setModalContent(<FightMenu setModalOpen={setModalOpen} />)
    }

    return (

        <GameWrapper>
            {modalOpen &&
                <ModalWrapper >
                    {modalContent}
                </ModalWrapper>
            }
            <SceneWrapper>
                <ButtonsWrapper>
                    <BigButton onClick={openBuildMenu}>
                        Build
                    </BigButton>
                    <ResourceTypeWrapper>
                        <ResourceType>
                            Resources
                        </ResourceType>
                        <ResourceWrapper>
                            {
                                buildingRes.map((resource, idx) => {
                                    return (
                                        <Resource key={`building_res_${resource.id}_${idx}`} type={resource.type} amount={resource.amount} />
                                    )
                                })
                            }
                        </ResourceWrapper>

                    </ResourceTypeWrapper>
                    <ResourceTypeWrapper>
                        <ResourceType>
                            Army
                        </ResourceType>
                        <ResourceWrapper>
                            {
                                armyRes.map((unitType, idx) => {
                                    return (
                                        <Resource key={`army_res_${unitType.id}_${idx}`} type={unitType.type} amount={unitType.amount} />
                                    )
                                })
                            }
                        </ResourceWrapper>
                    </ResourceTypeWrapper>
                    <BigButton onClick={openFightMenu}>
                        Fight
                    </BigButton>
                </ButtonsWrapper>
            </SceneWrapper>

        </GameWrapper>
    );
}

export default Game