import axios from "axios"
import { errorToast } from "components/common/Toast/Toast"
import { useEffect, useState } from "react"
import { useDispatch, useSelector } from "react-redux"
import { getAll, getAllGathering } from "redux/slices/GatheringSlice"
import styled from "styled-components"
import { calcResourceName } from 'utils/SGUtils'

const StatusWrapper = styled.div`
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
    background: grey;
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
    color: darkgrey;
`

const GatheringStatus = (props) => {

    const dispatch = useDispatch()
    
    const gathering = useSelector(store => store.gatheringSliceReducer.gatherings)

    useEffect(() => {
        dispatch(getAllGathering())
    }, [])

    return (
        <StatusWrapper>
            {gathering &&
                <StatusCard key={`gathering_status_card`}>
                    <FirstRow>
                        {`Gathering ${calcResourceName(gathering.resourceType)}.`}
                    </FirstRow>
                    <SecondRow>
                        {`Approx. ${gathering.timeLeft} minutes left.`}
                    </SecondRow>

                </StatusCard>}
        </StatusWrapper>
    )
}

export default GatheringStatus