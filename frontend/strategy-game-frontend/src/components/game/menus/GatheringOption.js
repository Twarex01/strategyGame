import styled from "styled-components"
import { calcBuildingName, calcGatheringName, calcResourceName } from "utils/SGUtils"
import Slider from '@mui/material/Slider';
import { withStyles } from '@material-ui/core/styles';
import { useState } from "react";

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

const CustomSlider = withStyles({
    root: {
        height: 3,
        padding: "13px 0",
    },
    track: {
        height: 4,
        borderRadius: 2,
        color: "darkgrey",
    },
    rail: {
        color: "white",
    },
    thumb: {
        backgroundColor: "#fff",
        color: "#fff",
    },
})(Slider);

const GatheringOption = (props) => {

    const [value, setValue] = useState(1)

    return (
        <OptionWrapper onClick={() => props.setSelected({ idx: props.idx, amount: value })} selected={props.selected}>
            <FirstRow selected={props.selected}>
                {calcGatheringName(props.gathering.type)}
            </FirstRow>
            <SecondRow selected={props.selected}>
                <p>Grants</p>
                <p>{props.gathering.minimumBaseReward}</p>
                <p> to </p>
                <p>{props.gathering.maximumBaseReward}</p>
                <p>{calcResourceName(props.gathering.type)},</p>
                <p> with a multiplier of {props.gathering.timeMultiplier}.</p>
            </SecondRow>
            <ThirdRow>
                <CustomSlider
                    aria-label="Time"
                    defaultValue={30}
                    getAriaValueText={props.valuetext}
                    valueLabelDisplay="auto"
                    step={1}
                    marks
                    min={1}
                    max={props.gathering.maxTimeAllowed}
                    onChange={(e) => { setValue(e.target.value); props.setSelected({ idx: props.idx, amount: value }) }}
                />
            </ThirdRow>
        </OptionWrapper>
    )
}

export default GatheringOption