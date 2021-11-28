import styled from "styled-components"
import buildBackground from 'assets/images/build-background.jpg'
import { useEffect, useState } from "react"
import axios from "axios"
import { errorToast } from "components/common/Toast/Toast"

import BuildMenu from "components/game/menus/BuildMenu"

import { HubConnectionBuilder } from '@microsoft/signalr'
import { useSelector } from "react-redux"
import Resources from "components/game/resource/Resources"



const BuildPageWrapper = styled.div`
    width: 100%;
    display: flex;
    flex-direction: column;
    align-items: center;

    @media(min-width: 600px){
        
    }`
// https://wall.alphacoders.com/big.php?i=1061167
const InnerMenuWrapper = styled.div`
    margin: 2rem 0;
    width: 100%;
    max-width: 1000px;
    overflow: hidden;
    background-size: cover;
    background-position: center;
    background-image: url(${buildBackground}); 
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    box-shadow: 11px 11px 11px rgba(0, 0, 0, 0.3);

    @media(min-width: 600px){
        
    }
`

const BuildPage = () => {

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

    return (
        <BuildPageWrapper>
            <InnerMenuWrapper>
                <BuildMenu fetchData={fetchData}/>
                <Resources resources={resources}/>
            </InnerMenuWrapper>
        </BuildPageWrapper>
    );
}

export default BuildPage