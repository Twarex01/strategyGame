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

const ResourceTypeWrapper = styled.div`
    display: flex;
    flex-direction: column;
    align-items: center;
`

const ResourceType = styled.div`
    width: 100%;
    background-image: url(${textBackground});
    color: white;
    font-weight: 600;
    font-size: 1.25rem;
    line-height: 2rem;
    display: grid;
    place-items: center;
`

const ResourceWrapper = styled.div`
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    align-items: flex-end;

    @media(min-width: 600px){
        flex-direction: row;
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
