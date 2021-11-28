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
import Resources from "components/game/resource/Resources"


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

const ModalWrapper = styled.div`
    width: 100%;
    max-width: 750px;
    z-index: 100;
    background-image: url(${textBackground});
    border-radius: 22px;
    box-shadow: 22px 22px 22px rgba(0, 0, 0, 0.6);
    padding: 1rem;
    margin: 0 auto;
`



const Game = () => {

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

    useEffect(() => {
        const connection = new HubConnectionBuilder()
            .withUrl('https://localhost:44365/roundhub')
            .withAutomaticReconnect()
            .build();

        connection.start()
            .then(result => {
                console.log('Connected!');

                connection.on('TurnEnded', message => {
                    console.log("Turn Ended")
                    infoToast("A turn has ended!")
                    fetchData()
                });
            })
            .catch(e => console.log('Connection failed: ', e));
        return () => {
            if (connection) {
                connection.on('TurnEnded', null)
            }
        }
    }, []);

    return (
        <GameWrapper>
            <SceneWrapper>
                <Resources resources={resources} />
            </SceneWrapper>

        </GameWrapper>
    );
}

export default Game