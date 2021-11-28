import styled from "styled-components"
import background from "assets/images/login-background.jpg"
import axios from "axios"
import { useEffect, useState } from "react"
import GatheringOption from "./GatheringOption"
import Slider from '@mui/material/Slider';
import { withStyles } from '@material-ui/core/styles';
import { errorToast, successToast } from "components/common/Toast/Toast"

const MenuWrapper = styled.div`
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: flex-start;
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
    justify-content: flex-end;
`

const ButtonsWrapper = styled.div`
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
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
const BattleText = styled.p`
    font-size: 1.5rem;
    color: lightgrey;
`

const ListItem = styled.div`
    display: flex;
    flex-direction: row;
    justify-content: space-around;
    width: 100%;
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

    const fetchData = async () => {
        try {
            setLoading(true)
            let token = localStorage.getItem("token")

            const res = await axios.get(
                'https://localhost:44365/api/resource/all',
                {
                    headers: {
                        Authorization: `Bearer ${token}`
                    }
                }
            )
            setLoading(false)
            setResources(res.data)
        } catch (err) {
            console.log(err)
            setErr(err)
            errorToast(err)
        }
    }

    const calcUnits = () => {
        resources.forEach(resource => {
            if (resource.type === 4) setUnits(resource.amount)
        })
    }

    useEffect(() => {
        calcUnits()
    }, [resources])
    useEffect(() => {
        fetchData()
    }, [])

    const [gatheringOptions, setGatheringOptions] = useState([])
    const [errOptions, setErrOptions] = useState()
    const [loadingOptions, setLoadingOptions] = useState(false)

    const fetchDataOptions = async () => {
        try {
            setLoadingOptions(true)
            let token = localStorage.getItem("token")

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
        fetchData()
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
            let token = localStorage.getItem("token")
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
            let token = localStorage.getItem("token")

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
            <InfoWrapper>
                <LeftSide>
                    <SideTitle>Gathering options</SideTitle>
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
                    <SideTitle>Send your army to battle!</SideTitle>

                    <CustomSlider
                        aria-label="Time"
                        defaultValue={1}
                        valueLabelDisplay="auto"
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
                </ButtonsWrapper>
                <ButtonsWrapper>
                    <ActionButton onClick={handlePostFight}>Fight</ActionButton>
                </ButtonsWrapper>
            </InfoWrapper>
            <ButtonsWrapper>
                <ActionButton onClick={() => props.setModalOpen(false)}>Cancel</ActionButton>
            </ButtonsWrapper>
        </MenuWrapper>
    )
}

export default FightMenu