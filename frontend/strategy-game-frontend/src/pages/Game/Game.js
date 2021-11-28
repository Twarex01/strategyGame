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
import { useDispatch, useSelector } from "react-redux"

import { useNavigate } from "react-router"
import Resources from "components/game/resource/Resources"
import Connection from "signalr/Signalr"
import { getAll } from "redux/slices/ResourceSlice"
import FightStatus from "components/game/status/FightStatus"
import GatheringStatus from "components/game/status/GatherStatus"


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
    overflow: hidden;
    background-size: cover;
    background-position: center;
    background-image: url(${gameBackground});
    height: 80vh;
    position: relative;
    box-shadow: 11px 11px 11px rgba(0, 0, 0, 0.3);

`

const StatusWrapper = styled.div`
    width: 100%;
    display: flex;
    flex-direction: column;
    align-items: flex-start;
    margin-left: 2rem;
`

const Game = () => {
    const dispatch = useDispatch()

    useEffect(() => {
        dispatch(getAll())
    }, [])

    const resources = useSelector(store => store.resourceSliceReducer.get.response)



    return (
        <GameWrapper>
            <SceneWrapper>
                <StatusWrapper>
                    <GatheringStatus /> 
                    <FightStatus />

                </StatusWrapper>
                <Resources resources={resources} />
            </SceneWrapper>

        </GameWrapper>
    );
}

export default Game