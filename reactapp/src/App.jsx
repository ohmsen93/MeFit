import './App.css';
// Bootstrap CSS
import "bootstrap/dist/css/bootstrap.min.css";
// Bootstrap Bundle JS
import "bootstrap/dist/js/bootstrap.bundle.min";

import { BrowserRouter, Routes, Route } from "react-router-dom"
import KeycloakRoute from './Hoc/keycloakRoutes';
import { ROLES } from "./const/roles"
import Dashboard from "./Views/Dashboard"
import Login from "./Views/Login"
import Navbar from './Components/navbar/Navbar';
import Contributor from './Views/Contributor';
import UserProfile from './Views/UserProfile';
import { useAuth } from './Components/context/AuthenticateContext';

function App() {

  const {auth} = useAuth();

    return (
      <BrowserRouter>
      {auth && (
        <Navbar />
      )}
        <main className="container">
          <Routes>
            <Route path="/" element={<Login />} />
            <Route path="/dashboard" element={<KeycloakRoute role={ ROLES.Regular }> <Dashboard /> </KeycloakRoute>}/>
            <Route path="/contributor" element={<KeycloakRoute role={ ROLES.Contributor}> <Contributor /> </KeycloakRoute>}/>
            <Route path="/profile" element={<KeycloakRoute role={ ROLES.Regular}> <UserProfile /> </KeycloakRoute>}/>
          </Routes>
        </main>
      </BrowserRouter>
    );
  }
  
  export default App;
