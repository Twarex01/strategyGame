import styled from "styled-components"
import fightBackground from 'assets/images/fight-background.jpg'
import { useEffect, useState, useRef } from "react"

import { useDispatch, useSelector } from "react-redux"
import Resources from "components/game/resource/Resources"
import ConquerMenu from "components/game/menus/ConquerMenu"
import { calcFood } from "utils/SGUtils"
import { getAll } from "redux/slices/ResourceSlice"

const ConquerPageWrapper = styled.div`
    width: 100%;
    display: flex;
    flex-direction: column;
    align-items: center;

    @media(min-width: 600px){
        
    }
`

const InnerMenuWrapper = styled.div`
    margin: 2rem 0;
    width: 100%;
    max-width: 1000px;
    overflow: hidden;
    background-size: cover;
    background-position: center;
    background-image: url(${fightBackground});
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    box-shadow: 11px 11px 11px rgba(0, 0, 0, 0.3);

    @media(min-width: 600px){
        
    }
`

const ConquerPage = () => {

    const dispatch = useDispatch()

    useEffect(() => {
        dispatch(getAll())
    }, [])

    const resources = useSelector(store => store.resourceSliceReducer.get.response)

    const [food, setFood] = useState(0)

    useEffect(() => {
        setFood(calcFood(resources))
    }, [resources])

    return (
        <ConquerPageWrapper>
            <InnerMenuWrapper>
                <ConquerMenu food={food} fetchData={() => dispatch(getAll())} />
                <Resources resources={resources} />
            </InnerMenuWrapper>
        </ConquerPageWrapper>
    );
}

export default ConquerPage