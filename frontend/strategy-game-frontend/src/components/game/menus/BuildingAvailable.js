import styled from "styled-components"
import { calcBuildingName, calcResourceName } from "utils/SGUtils"

const OptionWrapper = styled.div`
    width: 100%;
    display: flex;
    flex-direction: column;
    align-items: center;
    border: 1px solid grey;
    box-shadow: 20px 20px 20px rgba(0, 0, 0, 0.3);
    margin: 0.5rem;
    padding: 0.5rem;
    border-radius: 22px;
    background: grey;

    
`
const FirstRow = styled.div`
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    width: 100%;
    color: white;
    font-size: 1.25rem;
    border-bottom: 1px solid darkgrey;
`

const SecondRow = styled.div`
    display: flex;
    flex-direction: row;
    justify-content: space-around;
    width: 100%;
    color: white;
`
const ThirdRow = styled.div`
    display: flex;
    flex-direction: row;
    justify-content: space-around;
    width: 100%;
    color: grey;
`

const BuildingAvailable = (props) => {


    return (
        <OptionWrapper onClick={() => props.setSelected()} selected={props.selected}>
            <FirstRow>
                {calcBuildingName(props.buildingType.factoryParameters.resourceType)} x {parseInt(props.buildingType.amount)}
            </FirstRow>
            <SecondRow>
                <p>Grants</p>
                <p>{parseInt(props.buildingType.factoryParameters.passiveIncome) * parseInt(props.buildingType.amount)}</p>
                <p>{calcResourceName(props.buildingType.factoryParameters.resourceType)}</p>
                <p> per round.</p>
            </SecondRow>
        </OptionWrapper>
    )
}

export default BuildingAvailable