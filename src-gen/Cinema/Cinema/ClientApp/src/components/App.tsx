import React, { Component } from 'react';
import { Redirect, Route, Switch } from 'react-router';
import { BrowserRouter as Router} from 'react-router-dom';
import LoginPage from '../pages/LoginPage';
import BookingPage from '../pages/BookingPage';
import ResourceOverviewPage from '../pages/management/ResourceOverviewPage';
import UserPage from '../pages/UserPage';
import BookingOverviewPage from '../pages/BookingOverviewPage';
import CinemaHallsOverviewPage from '../pages/management/CinemaHall/CinemaHallsOverviewPage';
import UpdateCinemaHallPage from '../pages/management/CinemaHall/UpdateCinemaHallPage';
import CreateCinemaHallPage from '../pages/management/CinemaHall/CreateCinemaHallPage';
import SeatsOverviewPage from '../pages/management/Seat/SeatsOverviewPage';
import UpdateSeatPage from '../pages/management/Seat/UpdateSeatPage';
import CreateSeatPage from '../pages/management/Seat/CreateSeatPage';
import ClientsOverviewPage from '../pages/management/Client/ClientsOverviewPage';
import UpdateClientPage from '../pages/management/Client/UpdateClientPage';
import CreateClientPage from '../pages/management/Client/CreateClientPage';
import AdminsOverviewPage from '../pages/management/Admin/AdminsOverviewPage';
import UpdateAdminPage from '../pages/management/Admin/UpdateAdminPage';
import CreateAdminPage from '../pages/management/Admin/CreateAdminPage';
import NightPlansOverviewPage from '../pages/management/NightPlan/NightPlansOverviewPage';
import UpdateNightPlanPage from '../pages/management/NightPlan/UpdateNightPlanPage';
import CreateNightPlanPage from '../pages/management/NightPlan/CreateNightPlanPage';

const App = () => {

  const render = () => {
    return <Router>
      <Switch>
      	<Route exact path="/management/CinemaHalls_overview" component={CinemaHallsOverviewPage}/>
      	<Route exact path="/management/CinemaHall_update/:id" component={UpdateCinemaHallPage}/>
      	<Route exact path="/management/CinemaHall_create" component={CreateCinemaHallPage}/>
      	<Route exact path="/management/Seats_overview" component={SeatsOverviewPage}/>
      	<Route exact path="/management/Seat_update/:id" component={UpdateSeatPage}/>
      	<Route exact path="/management/Seat_create" component={CreateSeatPage}/>
      	<Route exact path="/management/Clients_overview" component={ClientsOverviewPage}/>
      	<Route exact path="/management/Client_update/:id" component={UpdateClientPage}/>
      	<Route exact path="/management/Client_create" component={CreateClientPage}/>
      	<Route exact path="/management/Admins_overview" component={AdminsOverviewPage}/>
      	<Route exact path="/management/Admin_update/:id" component={UpdateAdminPage}/>
      	<Route exact path="/management/Admin_create" component={CreateAdminPage}/>
      	<Route exact path="/management/NightPlans_overview" component={NightPlansOverviewPage}/>
      	<Route exact path="/management/NightPlan_update/:id" component={UpdateNightPlanPage}/>
      	<Route exact path="/management/NightPlan_create" component={CreateNightPlanPage}/>
      	<Route exact path="/management/overview" component={ResourceOverviewPage}/>
        <Route exact path="/booking/:id/:type" component={BookingPage}/>
        <Route exact path="/userpage/:id/:type" component={UserPage}/>
        		<Route exact path="/bookingoverview/:id/:type" component={BookingOverviewPage}/>
        		<Route exact path="/login" component={LoginPage}/>
        		<Redirect to="/login"/>
      </Switch>
    </Router>
  }

  return render();

}

export default App;
