import styled from "styled-components"
import { calcName } from "utils/SGUtils"
import backgroundImage from 'assets/images/login-background.jpg'

const ResultEntryWrapper = styled.div`
    display: flex;
    flex-direction: row;
    padding: 1rem 0;
    justify-content: space-around;
    align-items: center;
    border-bottom: 1px solid grey;
    background-image: url(${backgroundImage});
`

const ResultRank = styled.p`
    color: lightgrey;
    font-size: 1.5rem;
    font-weight: 600;
`
const ResultName = styled.p`
    color: white;
    font-size: 1.5rem;
    font-weight: 600;
`
const ResultScore = styled.p`
    color: black;
    font-size: 1.25rem;
    font-weight: 600;
`

const ResultEntry = (props) => {
    return (
        <ResultEntryWrapper>
            <ResultRank>{props.rank + 1}.</ResultRank>
            <ResultName> {calcName(props.email)}</ResultName>
            <ResultScore>{props.score}</ResultScore>
        </ResultEntryWrapper>
    )
}

export default ResultEntry