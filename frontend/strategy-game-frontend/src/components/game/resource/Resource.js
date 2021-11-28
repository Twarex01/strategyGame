import styled from "styled-components"
import wood from 'assets/resources/wood.png'
import food from 'assets/resources/food.png'
import stone from 'assets/resources/stone.png'
import iron from 'assets/resources/iron.png'
import gold from 'assets/resources/gold.png'
import atk from 'assets/resources/captain.png'
import soldier from 'assets/resources/soldier.png'
import { useEffect, useState } from "react"

const ResourceWrapper = styled.div`
    width: 75px;
    font-size: 1.25rem;
    display: flex;
    flex-direction: row;
    align-items: center;
    justify-content: space-around;
    border: 1px solid white;

    &:hover {
        cursor: pointer;
    }

`
const ResourceIconWrapper = styled.div`
    display: grid;
    place-items: center;
`

const ResourceAmount = styled.p`
    color: white;
`
const ResourceName = styled.p`
    color: white;
`

const ResourceIcon = styled.img`
    width: 100%;
    height: auto;
`

const calcResourceImg = (typeNum) => {
    let src = null
    switch (typeNum) {
        case 0:
            src = gold
            break;
        case 1:
            src = wood
            break;
        case 2:
            src = iron
            break;
        case 3:
            src = food
            break;
        case 4:
            src = atk
            break;
        default:
            src = wood
            break;
    }
    return src
}
const calcResourceName = (typeNum) => {
    let src = null
    switch (typeNum) {
        case 0:
            src = 'Gold'
            break;
        case 1:
            src = 'Wood'
            break;
        case 2:
            src = 'Iron'
            break;
        case 3:
            src = 'Food'
            break;
        case 4:
            src = 'Army'
            break;
        default:
            src = 'Wood'
            break;
    }
    return src
}

const resourceIcon = (type) => {

    return (
        <ResourceIconWrapper>
            <ResourceIcon src={calcResourceImg(type)} />
        </ResourceIconWrapper>
    )

}


const Resource = (props) => {

    const [res, setRes] = useState({
        res: wood,
        name: 'wood',
        amount: props.amount,
    })

    const calcRes = () => {
        setRes(
            {
                res: props.type,
                amount: props.amount,
                name: calcResourceName(props.type)
            }
        )
    }

    useEffect(() => {
        calcRes()
    }, [props])

    const [visible, setVisible] = useState(false)

    return (
        <ResourceWrapper onMouseEnter={() => setVisible(true)} onMouseLeave={() => setVisible(false)}>
            {visible ?
                <ResourceName>{res.name}</ResourceName>
                :
                <>
                    {resourceIcon(res.res)}
                    <ResourceAmount>{res.amount}</ResourceAmount>
                </>
            }
        </ResourceWrapper>
    )
}

export default Resource
