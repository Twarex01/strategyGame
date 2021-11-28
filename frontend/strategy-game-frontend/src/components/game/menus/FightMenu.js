import styled from "styled-components"
import background from "assets/images/login-background.jpg"
import axios from "axios"
import { useEffect, useState } from "react"
import GatheringOption from "./GatheringOption"
import Slider from '@mui/material/Slider';
import { withStyles } from '@material-ui/core/styles';
import { errorToast, successToast } from "components/common/Toast/Toast"

import { useSelector } from "react-redux"


const MenuWrapper = styled.div`
    display: flex;
    flex-direction: column;
    justify-content: flex-start;
    align-items: center;
    padding-bottom: 1rem;
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

    @media(min-width: 600px){
        display: flex;
        flex-direction: row;
        align-items: flex-start;
        justify-content: space-between;
        width: 100%;
    }
`


const InfoWrapper = styled.div`
    display: flex;
    flex-direction: column;
    align-items: center;
    width: 100%;
    max-height: 50vh;
    overflow-y: auto;

    @media(min-width: 600px){
        display: flex;
        flex-direction: row;
        align-items: flex-start;
        justify-content: space-between;
        width: 100%;
        max-height: 50vh;
        overflow-y: auto;
    }

`

const LeftSide = styled.div`
    padding-left: 1rem;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: space-around;
    @media(min-width: 600px){
        align-items: flex-start;
        padding-left: 2rem;
    }
`

const RightSide = styled.div`
    padding-right: 1rem;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: space-around;
    @media(min-width: 600px){
        align-items: flex-end;
        padding-right: 2rem;

    }
`

const ButtonsWrapper = styled.div`
    display: flex;
    flex-direction: row;
    align-items: center;
    justify-content: space-around;
    width: 100%;
    margin-top: 1rem;
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
const BattleText = styled.p`
    font-size: 1.5rem;
    color: lightgrey;
`

const CustomSlider = withStyles({
    root: {
        height: 3,
        marginTop: '5rem',
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
    }
})(Slider);

const FightMenu = (props) => {

    const [units, setUnits] = useState()
    const [resources, setResources] = useState([])
    const [err, setErr] = useState()
    const [loading, setLoading] = useState(false)

    const token = useSelector(store => store.persistedReducers.headerSliceReducer.token)


    const calcUnits = () => {
        resources.forEach(resource => {
            if (resource.type === 4) setUnits(resource.amount)
        })
    }

    useEffect(() => {
        calcUnits()
    }, [resources])

    const [gatheringOptions, setGatheringOptions] = useState([])
    const [errOptions, setErrOptions] = useState()
    const [loadingOptions, setLoadingOptions] = useState(false)

    const fetchDataOptions = async () => {
        try {
            setLoadingOptions(true)

            const res = await axios.get(
                'https://localhost:44365/api/command/gather/actions',
                {
                    headers: {
                        Authorization: `Bearer ${token}`
                    }
                }
            )
            setLoadingOptions(false)
            setGatheringOptions(res.data)
        } catch (err) {
            console.log(err)
            errorToast(err)
            setErrOptions(err)
        }
    }

    useEffect(() => {
        fetchDataOptions()
    }, [])

    const [selected, setSelected] = useState({ idx: 0, amount: 1 })

    useEffect(() => {
        console.log(selected)
    }, [selected])


    const [postGatherRes, setPostGatherRes] = useState()
    const [postGatherErr, setPostGatherErr] = useState()
    const [postGatherLoading, setPostGatherLoading] = useState(false)

    const handlePostGather = async () => {
        try {
            setPostGatherLoading(true)
            let body = {
                gatherId: `${gatheringOptions[selected.idx].id}`,
                time: `${selected.amount}`,
            }
            console.log(body)
            const res = await axios.post(
                'https://localhost:44365/api/command/gather',
                body,
                {
                    headers: {
                        Authorization: `Bearer ${token}`
                    }
                }
            )
            if (res.status === 200) {
                successToast("Operation successful!")
                props.fetchData()
                fetchDataOptions()
            }
            setPostGatherLoading(false)
            setPostGatherRes(res.data)
            console.log(res)
        } catch (err) {
            errorToast(err)
            setPostGatherErr(err)
        }
    }

    const [atkAmount, setAtkAmount] = useState(1)

    const [postFightRes, setPostFightRes] = useState()
    const [postFightErr, setPostFightErr] = useState()
    const [postFightLoading, setPostFightLoading] = useState(false)

    const handlePostFight = async () => {
        try {
            setPostFightLoading(true)

            const res = await axios.post(
                'https://localhost:44365/api/command/attack',
                {
                    atk: `${atkAmount}`
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
                fetchDataOptions()
            }
            setPostFightLoading(false)
            setPostFightRes(res.data)
            console.log(res)
        } catch (err) {
            errorToast(err)
            setPostFightErr(err)
        }
    }

    return (
        <MenuWrapper>
            <TitleWrapper>
                <LeftSide>
                    <SideTitle>Gathering options</SideTitle>
                </LeftSide>
                <RightSide>
                    <SideTitle>Send your army to battle!</SideTitle>
                </RightSide>
            </TitleWrapper>
            <InfoWrapper>
                <LeftSide>
                    <MobileTitleWrapper>
                        <SideTitle>Gathering options</SideTitle>
                    </MobileTitleWrapper>

                    <ListWrapper>

                        {
                            gatheringOptions.map((gathering, idx) => {
                                return (
                                    <GatheringOption key={`gathering_available_${gathering.id}_${idx}`} idx={idx} setSelected={setSelected} selected={selected.idx === idx} gathering={gathering} />
                                )
                            })
                        }
                    </ListWrapper>

                </LeftSide>
                <RightSide>
                    <MobileTitleWrapper>
                        <SideTitle>Send your army to battle!</SideTitle>
                    </MobileTitleWrapper>

                    <CustomSlider
                        aria-label="Time"
                        defaultValue={1}
                        step={1}
                        min={1}
                        valueLabelDisplay="on"
                        max={units ? units : 1}
                        onChange={(e) => { setAtkAmount(e.target.value) }}
                    />
                    <BattleText>Send {atkAmount} units to battle!</BattleText>
                </RightSide>
            </InfoWrapper>
            <InfoWrapper>
                <ButtonsWrapper>
                    <ActionButton onClick={handlePostGather}>Gather</ActionButton>
                    <ActionButton onClick={handlePostFight}>Fight</ActionButton>
                </ButtonsWrapper>
            </InfoWrapper>
        </MenuWrapper>
    )
}

export default FightMenu