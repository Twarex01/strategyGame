import { HubConnectionBuilder } from '@microsoft/signalr'

const token = localStorage.getItem("token")

const Connection = new HubConnectionBuilder()
    .withUrl(`https://localhost:44365/roundhub?access_token=${token}`)
    .withAutomaticReconnect()
    .build();

Connection.start()
    .then(result => {
        console.log('SignalR onnected!');
    })
    .catch(e => console.log('Connection failed: ', e));

export default Connection