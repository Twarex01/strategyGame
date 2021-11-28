import styled from "styled-components"
import { useEffect, useState } from "react"
import textBackground from "assets/images/login-background.jpg"
import axios from "axios"
import { useSelector } from "react-redux"
import { errorToast } from "components/common/Toast/Toast"
import Resource from "./Resource"

const ResourcesWrapper = styled.div`
    background: #262729;
    
    width: 100%;
    display: flex;
    flex-direction: row;
    align-items: center;
    justify-content: space-around;

    @media(min-width: 600px){
        width: 100%;
        display: flex;
        flex-direction: row;
        justify-content: space-around;
        align-items: flex-end;
    }
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
                inset 5px 5px 5px rgba(255, 255, 255, 0.6);

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
    flex-direction: column;
    justify-content: space-between;
    align-items: flex-end;
    min-height: 25px;


    @media(min-width: 600px){
        display: flex;
        flex-direction: row;
        justify-content: space-between;
        align-items: flex-end;
        height: 25px;
    }

`


const Resources = (props) => {
    const [resources, setResources] = useState(props.resources? props.resources : [])

    useEffect(() => {
        setResources(props.resources)
    }, [props.resources])

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

    return (
        <ResourcesWrapper>
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

        </ResourcesWrapper>
    )
}

export default Resources
