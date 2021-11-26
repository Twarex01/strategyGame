import styled from "styled-components"
import gameBackground from 'assets/images/game-background.jpg'

const GameWrapper = styled.div`
    padding: 2rem;
    width: 100%;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: space-around;
    border: 1px solid red;
`

const SceneWrapper = styled.div`
    width: 100%;
    max-width: 1000px;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: flex-end;
    border: 1px solid red;
    background-image: url(${gameBackground});
    height: 80vh;
    position: relative;
`

const ButtonsWrapper = styled.div`
    width: 100%;
    display: flex;
    flex-direction: row;
    justify-content: space-around;
    align-items: flex-end;
    background: #262729;
`

const BigButton = styled.div`
    display: grid;
    place-items: center;
    width: 100px;
    height: 100px;
    border-radius: 50%;
    border: 1px solid black;
    margin: 1rem 0;
    background: #D7D9D7;
`

const ResourceTypeWrapper = styled.div`
    display: flex;
    flex-direction: column;
    align-items: center
`

const ResourceType = styled.div`
    width: 100%;
    height: 25px;
    background: red;
    display: grid;
    place-items: center;
`

const ResourceWrapper = styled.div`
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: flex-end;
    border: 1px solid grey;
    height: 25px;
`

const Resource = styled.div`
    width: 75px;
    height: 25px;
    border: 1px solid red;
`

const Game = () => {

    return (

        <GameWrapper>
            <SceneWrapper>
                <ButtonsWrapper>
                    <BigButton>
                        Build
                    </BigButton>
                    <ResourceTypeWrapper>
                        <ResourceType>
                            Resources
                        </ResourceType>

                        <ResourceWrapper>
                            <Resource > a</Resource>
                            <Resource > b</Resource>
                            <Resource > c</Resource>
                        </ResourceWrapper>

                    </ResourceTypeWrapper>
                    <ResourceTypeWrapper>
                        <ResourceType>
                            Army
                        </ResourceType>

                        <ResourceWrapper>
                            <Resource > d</Resource>
                            <Resource > e</Resource>

                        </ResourceWrapper>
                    </ResourceTypeWrapper>
                    <BigButton>
                        Fight
                    </BigButton>
                </ButtonsWrapper>
            </SceneWrapper>

        </GameWrapper>
    );
}

export default Game