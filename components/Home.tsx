import React, { Component } from 'react';
import { Container, Table } from 'reactstrap';
import { useAuth0 } from '@auth0/auth0-react';
import Hero from './Hero';

interface order {
    company: string,
    title: string,
    arrival: Date,
    link: string
}

var orders: order[] = [{ company: 'DHL', title: "Roastmarket", arrival: new Date("2020-08-01"), link: "https://www.dhl.de/de/privatkunden/pakete-empfangen/verfolgen.html?zip=60323&idc=00340434289685231453" },
{ company: 'DPD', title: "Saturn", arrival: new Date("2020-08-02"), link: "https://www.dhl.de/de/privatkunden/pakete-empfangen/verfolgen.html?zip=60323&idc=00340434289685231453" },
{ company: 'Hermes', title: "Amazon", arrival: new Date("2020-08-03"), link: "https://www.dhl.de/de/privatkunden/pakete-empfangen/verfolgen.html?zip=60323&idc=00340434289685231453" }];

const Home = () => {
    const {
        user,
        isAuthenticated,
        loginWithRedirect,
        logout,
    } = useAuth0();

    return (
        <Container>
            {!isAuthenticated && (<Hero></Hero>)}
            {isAuthenticated && (
                <Table responsive striped className="text-center">
                    <thead>
                        <tr>
                            <th scope="col">Description</th>
                            <th scope="col">Arrival Date</th>
                            <th scope="col">Tracking link</th>
                        </tr>
                    </thead>
                    <tbody>
                        {orders && orders.map((order: order, index) => (
                            <tr key={index}>
                                <td>
                                    <p>{order.title} ({order.company})</p>
                                </td>
                                <td>
                                    <p>{order.arrival.toLocaleDateString()}</p>
                                </td>
                                <td><a className="btn btn-secondary" href={order.link} target="_blank"><b>SHOW DETAILS</b></a></td>
                            </tr>
                        ))}
                    </tbody>
                </Table>
            )}
        </Container>
    );
}

export default Home;