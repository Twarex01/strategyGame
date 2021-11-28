import styled from "styled-components"
import background from "assets/images/login-background.jpg"
import axios from "axios"
import { useEffect, useState } from "react"
import BuildingOption from 'components/game/menus/BuildingOption'
import BuildingAvailable from "./BuildingAvailable"
import { errorToast, successToast } from "components/common/Toast/Toast"

const MenuWrapper = styled.div`
    display: flex;
    flex-direction: column;
    justify-content: flex-start;
    align-items: center;
    z-index: 1000;
`

const InfoWrapper = styled.div`
    display: flex;
    flex-direction: row;
    align-items: flex-start;
    justify-content: space-between;
    width: 100%;
    padding: 2rem;
`

const LeftSide = styled.div`
    padding-left: 1rem;
    display: flex;
    flex-direction: column;
    align-items: flex-start;
    justify-content: space-around;
`

const RightSide = styled.div`
    padding-right: 1rem;
    display: flex;
    flex-direction: column;
    align-items: flex-end;
    justify-content: space-around;
`

const ButtonsWrapper = styled.div`
    display: flex;
    flex-direction: row;
    align-items: center;
    justify-content: space-around;
    width: 100%;
`

const ActionButton = styled.div`
    border-radius: 22px;
    padding: 1.125rem 1.75rem;
    color: white;
    background-image: url(${background});

    &:hover {
        cursor: pointer;
    }
`

const ListWrapper = styled.div`
    display: flex;
    flex-direction: column;
    align-items: center;
    width: 100%;
`

const SideTitle = styled.p`
    font-size: 1.5rem;
    color: darkgrey;
`

const ListItem = styled.div`
    display: flex;
    flex-direction: row;
    justify-content: space-around;
    width: 100%;
`

const BuildMenu = (props) => {

    const [buildings, setBuildings] = useState([])
    const [err, setErr] = useState()
    const [loading, setLoading] = useState(false)

    const fetchData = async () => {
        try {
            setLoading(true)
            let token = localStorage.getItem("token")

            const res = await axios.get(
                'https://localhost:44365/api/command/build/actions',
                {
                    headers: {
                        Authorization: `Bearer ${token}`
                    }
                }
            )
            setLoading(false)
            setBuildings(res.data)
        } catch (err) {
            console.log(err)
            setErr(err)
            errorToast(err)
        }
    }


    const [buildingsAvailable, setBuildingsAvailable] = useState([])
    const [errAvailable, setErrAvailable] = useState()
    const [loadingAvailable, setLoadingAvailable] = useState(false)

    const fetchDataAvailable = async () => {
        try {
            setLoadingAvailable(true)
            let token = localStorage.getItem("token")

            const res = await axios.get(
                'https://localhost:44365/api/building/all',
                {
                    headers: {
                        Authorization: `Bearer ${token}`
                    }
                }
            )
            setLoadingAvailable(false)
            setBuildingsAvailable(res.data)
        } catch (err) {
            console.log(err)
            errorToast(err)
            setErrAvailable(err)
        }
    }

    useEffect(() => {
        fetchData()
        fetchDataAvailable()
    }, [])

    const [selected, setSelected] = useState(0)
    useEffect(() => {
        console.log(buildings[selected]?.id)
    }, [selected])
    const [postRes, setPostRes] = useState()
    const [postErr, setPostErr] = useState()
    const [postLoading, setPostLoading] = useState(false)

    const handlePost = async () => {
        try {
            setPostLoading(true)
            let token = localStorage.getItem("token")

            const res = await axios.post(
                'https://localhost:44365/api/command/build',
                {
                    buildingId: `${buildings[selected].id}`
                },
                {
                    headers: {
                        Authorization: `Bearer ${token}`
                    }
                }
            )
            setPostLoading(false)
            setPostRes(res.data)
            console.log(res)
            if (res.status === 200) {
                successToast("Operation successful!")
            }
        } catch (err) {
            setPostErr(err)
            errorToast(err)
        }
    }

    return (
        <MenuWrapper>
            <InfoWrapper>
                <LeftSide>
                    <SideTitle>You currently have:</SideTitle>

                    <ListWrapper>

                        {
                            buildingsAvailable.map((buildingType, idx) => {
                                return (
                                    <BuildingAvailable key={`building_available_${buildingType.id}_${idx}`} setSelected={() => { }} buildingType={buildingType} />
                                )
                            })
                        }
                    </ListWrapper>
                </LeftSide>
                <RightSide>
                    <SideTitle>Select a building to build</SideTitle>

                    <ListWrapper>
                        {
                            buildings.map((building, idx) => {
                                return (
                                    <BuildingOption key={`building_option_${building.id}_${idx}`} setSelected={() => setSelected(idx)} selected={selected === idx} building={building} />
                                )
                            })
                        }
                    </ListWrapper>

                </RightSide>
            </InfoWrapper>
            <ButtonsWrapper>
                <ActionButton onClick={() => props.setModalOpen(false)}>Cancel</ActionButton>
                <ActionButton onClick={handlePost}>Build</ActionButton>
            </ButtonsWrapper>
        </MenuWrapper>
    )
}

export default BuildMenu