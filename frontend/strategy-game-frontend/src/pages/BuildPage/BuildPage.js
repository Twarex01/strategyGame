import styled from "styled-components"
import buildBackground from 'assets/images/build-background.jpg'
import { useEffect, useState } from "react"
import axios from "axios"
import { errorToast } from "components/common/Toast/Toast"

import BuildMenu from "components/game/menus/BuildMenu"

import { HubConnectionBuilder } from '@microsoft/signalr'
import { useDispatch, useSelector } from "react-redux"
import Resources from "components/game/resource/Resources"
import { getAll } from "redux/slices/ResourceSlice"



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
    const dispatch = useDispatch()

    useEffect(() => {
        dispatch(getAll())
    }, [])

    const resources = useSelector(store => store.resourceSliceReducer.get.response)

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
                <BuildMenu fetchData={() => dispatch(getAll())}/>
                <Resources resources={resources}/>
            </InnerMenuWrapper>
        </BuildPageWrapper>
    );
}

export default BuildPage