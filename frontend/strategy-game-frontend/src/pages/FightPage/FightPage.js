import styled from "styled-components"
import fightBackground from 'assets/images/fight-background.jpg'
import Resource from "components/game/resource/Resource"
import { useEffect, useState, useRef } from "react"
import axios from "axios"
import { errorToast, infoToast, warningToast } from "components/common/Toast/Toast"

import BuildMenu from "components/game/menus/BuildMenu"
import FightMenu from "components/game/menus/FightMenu"

import { HubConnectionBuilder } from '@microsoft/signalr'
import { useSelector } from "react-redux"

import { useNavigate } from "react-router"
import Resources from "components/game/resource/Resources"

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
        <FightPageWrapper>
            <InnerMenuWrapper>
                <FightMenu fetchData={fetchData} />
                <Resources resources={resources} />
            </InnerMenuWrapper>
        </FightPageWrapper>
    );
}

export default FightPage