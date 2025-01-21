import React from 'react'
import { Container,  Content} from 'rsuite';
import { Outlet} from 'react-router-dom';
import CarList from "./Components/CarList";
import './App.css'


const App: React.FC =() =>
{

  return (
    <div id="root">
      <Container className='container'>  
        <Content className="content" >
          <CarList/>
          <Outlet />
        </Content>
        </Container>
    </div>
  )
}

export default App
