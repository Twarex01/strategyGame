import styled from "styled-components"
import fightBackground from 'assets/images/fight-background.jpg'
import Resource from "components/game/resource/Resource"
import { useEffect, useState, useRef } from "react"
import axios from "axios"
import { errorToast, infoToast, warningToast } from "components/common/Toast/Toast"

import BuildMenu from "components/game/menus/BuildMenu"
import FightMenu from "components/game/menus/FightMenu"

import { HubConnectionBuilder } from '@microsoft/signalr'
import { useDispatch, useSelector } from "react-redux"

import { useNavigate } from "react-router"
import Resources from "components/game/resource/Resources"
import { getAll } from "redux/slices/ResourceSlice"

const FightPageWrapper = styled.div`
    width: 100%;
    display: flex;
    flex-direction: column;
    align-items: center;

    @media(min-width: 600px){
        
    }`

const InnerMenuWrapper = styled.div`
    margin: 2rem 0;
    width: 100%;
    max-width: 1000px;
    overflow: hidden;
    background-size: cover;
    background-position: center;
    background-image: url(${fightBackground});
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    box-shadow: 11px 11px 11px rgba(0, 0, 0, 0.3);

    @media(min-width: 600px){
        
    }
`

const FightPage = () => {
    const dispatch = useDispatch()

    useEffect(() => {
        dispatch(getAll())
    }, [])

    const resources = useSelector(store => store.resourceSliceReducer.get.response)

    return (
        <FightPageWrapper>
            <InnerMenuWrapper>
                <FightMenu resources={resources} fetchData={() => dispatch(getAll())} />
                <Resources resources={resources} />
            </InnerMenuWrapper>
        </FightPageWrapper>
    );
}

export default FightPage