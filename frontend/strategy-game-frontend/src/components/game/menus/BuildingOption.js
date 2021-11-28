import styled from "styled-components"
import { calcBuildingName, calcResourceName } from "utils/SGUtils"

const OptionWrapper = styled.div`
    width: 100%;
    display: flex;
    flex-direction: column;
    align-items: center;
    border: 1px solid grey;
    box-shadow: 20px 20px 20px rgba(0, 0, 0, 0.3);
    margin: 0.25rem;
    padding: 0.5rem;
    border-radius: 22px;
    background: ${props => props.selected ? "lightgrey" : "grey"};

    &:hover {
        cursor: pointer;
    }
`
const FirstRow = styled.div`
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    width: 100%;
    color: ${props => props.selected ? "black" : "lightgrey"};
    font-size: 1.25rem;
    border-bottom: 1px solid darkgrey;
`

const SecondRow = styled.div`
    display: flex;
    flex-direction: row;
    justify-content: space-around;
    width: 100%;
    color: ${props => props.selected ? "grey" : "lightgrey"};
`
const ThirdRow = styled.div`
    display: flex;
    flex-direction: row;
    justify-content: space-around;
    width: 100%;
    color: ${props => props.selected ? "grey" : "lightgrey"};
`



const BuildingOption = (props) => {

    return (
        <OptionWrapper onClick={() => props.setSelected()} selected={props.selected}>
            <FirstRow selected={props.selected}>
                {calcBuildingName(props.building.factoryParameters.resourceType)}
            </FirstRow>
            <SecondRow selected={props.selected}>
                <p>Grants</p>
                <p>{props.building.factoryParameters.passiveIncome}</p>
                <p>{calcResourceName(props.building.factoryParameters.resourceType)}</p>
                <p> per round.</p>
            </SecondRow>
            {
                props.building.buildingPrice.map(cost => {
                    return (
                        <ThirdRow  key={`building_option_${props.building.id}_price_${cost.key}`} selected={props.selected}>
                            <p>Costs</p>
                            <p>{cost.value}</p>
                            <p>{calcResourceName(cost.key)}</p>
                            <p> to build.</p>
                        </ThirdRow>
                    )
                })
            }

        </OptionWrapper>
    )
}

export default BuildingOption