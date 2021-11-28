import axios from "axios"
import { errorToast } from "components/common/Toast/Toast"
import { useEffect, useState } from "react"
import { useDispatch, useSelector } from "react-redux"
import { getAllFights } from "redux/slices/FightSlice"
import styled from "styled-components"

const StatusWrapper = styled.div`
    width: 100%;
    max-width: 350px;
    display: flex;
    flex-direction: column;
    align-items: center;
    margin-bottom: 2rem;
    background: grey;
    border-radius: 22px;
    box-shadow: 11px 11px 11px rgba(0, 0, 0, 0.3);
`
const ListWrapper = styled.div`
    width: 100%;
    max-width: 350px;
    display: flex;
    flex-direction: column;
    align-items: center;
    margin-bottom: 2rem;
    max-height: 20vh;
    overflow-y: auto;
`
const StatusCard = styled.div`
    width: 100%;
    max-width: 300px;
    display: flex;
    flex-direction: column;
    align-items: center;
    border: 1px solid lightgrey;
    border-radius: 22px;
    background: darkgrey;
    box-shadow: 11px 11px 11px rgba(0, 0, 0, 0.3);
    margin: 1rem 0;
`
const FirstRow = styled.div`
    display: flex;
    flex-direction: row;
    align-items: center;
    justify-content: flex-start;
    color: white;
    font-size: 1.25rem;
    font-weight: 600;
`
const SecondRow = styled.div`
    display: flex;
    flex-direction: row;
    align-items: center;
    justify-content: flex-end;
    color: lightgrey;
`
const TitleWrapper = styled.div`
    background: #262729;
    width: 100%;
    text-align: center;
    border-radius: 22px 22px 0 0;
`
const StatusTitle = styled.p`
    color: white;
    font-size: 1.25rem;
    font-weight: 600;
`

const FightStatus = (props) => {

    const dispatch = useDispatch()

    const fights = useSelector(store => store.fightSliceReducer.fights)

    useEffect(() => {
        dispatch(getAllFights())
    }, [dispatch])

    return (
        <StatusWrapper>
            {(fights.length !== 0) &&
                <>
                    <TitleWrapper>
                        <StatusTitle>
                            Ongoing fights
                        </StatusTitle>
                    </TitleWrapper>
                    <ListWrapper>

                        {
                            fights.map((fight, idx) => {
                                return (
                                    <StatusCard key={`fight_status_card_${idx}`}>
                                        <FirstRow>
                                            {`${fight.atkPower} units fighting.`}
                                        </FirstRow>
                                        <SecondRow>
                                            {`Approx. ${fight.timeLeft} minutes left.`}
                                        </SecondRow>

                                    </StatusCard>
                                )
                            })
                        }
                    </ListWrapper>
                </>
            }

        </StatusWrapper>
    )
}

export default FightStatus