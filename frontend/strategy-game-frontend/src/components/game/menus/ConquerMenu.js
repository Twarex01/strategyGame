import styled from "styled-components"
import background from "assets/images/login-background.jpg"
import axios from "axios"
import { useEffect, useState } from "react"
import BuildingOption from 'components/game/menus/BuildingOption'
import BuildingAvailable from "./BuildingAvailable"
import { errorToast, successToast } from "components/common/Toast/Toast"

import { useSelector } from "react-redux"
import { Slide, Slider, withStyles } from "@material-ui/core"

const MenuWrapper = styled.div`
    display: flex;
    flex-direction: column;
    justify-content: flex-start;
    align-items: center;
`

const MobileTitleWrapper = styled.div`
    display: flex;
    flex-direction: row;
    align-items: flex-start;
    justify-content: space-between;
    width: 100%;

    @media(min-width: 600px){
        display: none;
    }
`

const TitleWrapper = styled.div`
    display: none;
    background: #262729;
    margin-bottom: 1rem;

    @media(min-width: 600px){
        display: flex;
        flex-direction: column;
        align-items: center;
        width: 100%;
    }
`
const InfoWrapper = styled.div`
    display: flex;
    flex-direction: column;
    align-items: center;
    max-height: 60vh;
    overflow-y: auto;
    padding: 2rem;
`

const LeftSide = styled.div`
    padding-left: 1rem;
    display: flex;
    flex-direction: column;
    align-items: flex-start;
    justify-content: space-around;
    @media(min-width: 600px){
        align-items: flex-end;
        padding-left: 2rem;

    }
`

const RightSide = styled.div`
    padding-right: 1rem;
    display: flex;
    flex-direction: column;
    align-items: flex-end;
    justify-content: space-around;
    @media(min-width: 600px){
        align-items: flex-end;
        padding-right: 2rem;

    }
`

const ButtonsWrapper = styled.div`
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    width: 100%;
    padding: 1rem 0;
    background: #262729;
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

const SideTitle = styled.p`
    font-size: 1.5rem;
    color: darkgrey;
`

const ConquerText = styled.p`
    font-size: 1.5rem;
    color: lightgrey;
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

const ConquerMenu = (props) => {

    const token = useSelector(store => store.persistedReducers.headerSliceReducer.token)

    const [amount, setAmount] = useState(1)

    const [postRes, setPostRes] = useState()
    const [postErr, setPostErr] = useState()
    const [postLoading, setPostLoading] = useState(false)

    const handlePost = async () => {
        try {
            setPostLoading(true)

            const res = await axios.post(
                'https://localhost:44365/api/command/trade',
                {
                    tradeId: "8fe04f5a-915d-434f-8a1c-08d9b0cc4d10",
                    amount: `${amount}`
                },
                {
                    headers: {
                        Authorization: `Bearer ${token}`
                    }
                }
            )
            if (res.status === 200) {
                successToast("Operation successful!")
                props.fetchData()
            }
            setPostLoading(false)
            setPostRes(res.data)
            console.log(res)
        } catch (err) {
            errorToast(err)
            setPostErr(err)
        }
    }

    return (
        <MenuWrapper>
            <TitleWrapper>
                <SideTitle>Conquer new lands</SideTitle>
            </TitleWrapper>
            <InfoWrapper>
                <CustomSlider
                    aria-label="Food amount"
                    defaultValue={1}
                    getAriaValueText={props.valuetext}
                    valueLabelDisplay={true}
                    step={1}
                    min={1}
                    max={props.food}
                    onChange={(e, v) => setAmount(v)}
                />
                <ConquerText>Spend {amount} food to conquer new lands!</ConquerText>
            </InfoWrapper>
            <ButtonsWrapper>
                <ActionButton onClick={handlePost}>Conquer</ActionButton>
            </ButtonsWrapper>
        </MenuWrapper>
    )
}

export default ConquerMenu