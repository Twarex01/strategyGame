import axios from "axios"
import { useEffect, useState } from "react"
import styled from "styled-components"
import ResultEntry from "components/game/ResultEntry"
import background from "assets/images/login-background.jpg"
import { errorToast } from "components/common/Toast/Toast"

const ToplistWrapper = styled.div`
    padding: 2rem;
    width: 100%;
    display: flex;
    flex-direction: column;
    align-items: center;
`

const ListWrapper = styled.div`
    width: 100%;
    max-width: 500px;
    background-image: url(${background});
    border-radius: 22px;
`

const TitleText = styled.h1`
    width: 100%;
    padding: 0 0 2rem 0;
    text-align: center;
    border-bottom: 2px solid grey;
    height: 2rem;
`

const scoreComparer = (result1, result2) => {
    if (result1.score >= result2.score) return -1
    if (result1.score < result2.score) return 1
    return 0
}

const Toplist = () => {

    const [results, setResults] = useState([])
    const [err, setErr] = useState()
    const [loading, setLoading] = useState(false)

    const [resultsOrdered, setResultsOrdered] = useState([])

    const orderResults = () => {
        setResultsOrdered(results.sort(scoreComparer))
    }

    useEffect(() => {
        orderResults()
    }, [results])



    const fetchData = async () => {
        try {
            setLoading(true)
            const res = await axios.get(
                "https://localhost:44365/api/stats/score/all",
                {
                    headers: {
                        Authorization: `Bearer ${localStorage.getItem("token")}`
                    }
                }
            )
            setLoading(false)
            setResults(res.data)
        } catch (err) {
            console.log(err)
            errorToast(err)
            setErr(err)
        }
    }

    useEffect(() => {
        fetchData()
    }, [])

    return (

        <ToplistWrapper>
            <ListWrapper>
                <TitleText>
                    Welcome to the Hall of Fame!
                </TitleText>
                {
                    resultsOrdered.map((result, idx) => {
                        return (
                            <ResultEntry key={`results_${result.name}_${idx}`} rank={idx} email={result.playerEmail} score={result.score} />
                        )
                    })
                }
            </ListWrapper>
        </ToplistWrapper>
    );
}

export default Toplist