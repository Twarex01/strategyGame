import styled from "styled-components"

const LandingWrapper = styled.div`
    padding: 2rem;
    width: 100%;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: space-around;
`
const TitleWrapper = styled.div`
    widht: 100%;
    max-width: 1000px;
    text-align: center;
    margin: 1rem;

`
const DescriptionWrapper = styled.div`
    widht: 100%;
    max-width: 1000px;
    text-align: center;

`
const TitleText = styled.h1`
    height: 3rem;
`
const DescriptionText = styled.h2`
    height: 1.25rem;
`

const Landing = () => {

    return (

        <LandingWrapper>
            <TitleWrapper>
                <TitleText>
                    Welcome to the Strategy Game!
                </TitleText>
            </TitleWrapper>
            <DescriptionWrapper>
                <DescriptionText>
                    <iframe width="750" height="427" src="https://www.youtube.com/embed/pYk_zkA19Nw?autoplay=1" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
                </DescriptionText>
            </DescriptionWrapper>
        </LandingWrapper>
    );
}

export default Landing